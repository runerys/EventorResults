using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Eventor.Integration;
using Eventor.Schema;
using EventorResults.Core;
using PersonResult = Eventor.Schema.PersonResult;
using ResultList = Eventor.Schema.ResultList;

namespace EventorResults.ClubResultsForYear
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Crawl starting...");
            var events = ParseEvents(@"C:\eventor\events.txt");

            DownloadResultsFor(events);
            FixXmlFileFor(events);
            CreateTextFileFor(events);

            Console.WriteLine("Crawl completed. Press Enter to exit...");
            Console.ReadLine();
        }

        private static Event[] ParseEvents(string file)
        {
            var events = new List<Event>();
            var regex = new Regex(@"(\d+)(, \d+)?, (.*)");
            foreach (var l in File.ReadAllLines(file))
            {
                var line = l;

                if (string.IsNullOrEmpty(line))
                    continue;

                line = line.Trim();

                var match = regex.Match(line);

                if (!match.Success)
                    continue;

                var eventId = int.Parse(match.Groups[1].Value);
                var eventRaceId = match.Groups[2].Value;
                var name = match.Groups[3].Value;

                var e = new Event(eventId, name);

                if (name.Contains(", SE, "))
                    e = new EventSwe(eventId, name);

                if (!string.IsNullOrEmpty(eventRaceId))
                    e.EventRaceId = eventRaceId.Replace(", ", string.Empty);

                events.Add(e);

            }

            return events.ToArray();
        }

        private static void FixXmlFileFor(Event[] events)
        {
            foreach (var input in events)
            {
                var downloadedFile = BuildFilename(input.EventId);

                if (!File.Exists(downloadedFile))
                    continue; // Not downloaded

                var fixedFile = BuildFilenameFixed(input.EventId);

                if (File.Exists(fixedFile))
                    File.Delete(fixedFile);
                //continue; // Already processed

                var sb = new StringBuilder();
                var filecontent = File.ReadAllLines(downloadedFile);

                var formattedXml = FormatXml(string.Join(" ", filecontent));
                filecontent = formattedXml.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string previousLine = string.Empty;

                foreach (var line in filecontent)
                {
                    if (string.Equals(previousLine.Trim(), line.Trim(), StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    sb.AppendLine(line);
                    previousLine = line;
                }

                File.WriteAllText(fixedFile, sb.ToString());
            }
        }

        static string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        private static string BuildFilenameFixed(int eventId)
        {
            var filename = string.Format(@"c:\eventor\eventresults\{0}_fixed.xml", eventId);
            return filename;
        }

        private static void CreateTextFileFor(Event[] events)
        {
            var filename = @"c:\eventor\clubresults.txt";

            if (File.Exists(filename))
                File.Delete(filename);

            var sb = new StringBuilder();

            foreach (var input in events)
            {
                var eventId = input.EventId;

                string eventFile = BuildFilenameFixed(eventId);

                if (!File.Exists(eventFile))
                    eventFile = BuildFilename(eventId);

                var eventXml = File.ReadAllText(eventFile);

                var resultList = ApiSchemaReader.ReadAs<ResultList>(eventXml);

                var e = resultList.Item as Eventor.Schema.Event;

                if (e == null)
                    continue;

                var eventName = e.Name.Text[0];
                var eventDate = e.StartDate.Date;
                //sb.AppendFormat("{0}, {1:d. MMMM}", eventName, DateTime.Parse(eventDate.Text[0]));
                sb.Append(input.Shortname);
                sb.AppendLine();
                sb.AppendFormat("{0}/Events/Show/{1}", input.Config.EventorBaseUrl, input.EventId, input.Config.OrgId);
                sb.AppendLine();
                sb.AppendFormat("{0}/Events/ResultList?eventId={1}&organisationId={2}", input.Config.EventorBaseUrl, input.EventId, input.Config.OrgId);
                if (!string.IsNullOrEmpty(input.EventRaceId))
                    sb.Append($"&eventRaceId={input.EventRaceId}");
                sb.AppendLine();
                sb.AppendLine();

                if (resultList.ClassResult == null)
                    continue;

                // Klasse
                foreach (var classResult in resultList.ClassResult)
                {
                    var eventClass = classResult.Item as Eventor.Schema.EventClass;
                    var anyFromClub = false;
                    var classBuilder = new StringBuilder();

                    classBuilder.AppendLine(eventClass.Name.Text[0]);

                    if (classResult.Items.Any())
                    {
                        if (classResult.Items.First() is PersonResult)
                        {
                            foreach (var item in classResult.Items)
                            {
                                var v = PrintPersonResult((PersonResult)item, classBuilder, anyFromClub, input);
                                if (v.HasValue)
                                    anyFromClub = v.Value;
                            }
                        }
                        else if (classResult.Items.First() is TeamResult)
                        {
                            anyFromClub = PrintTeamResults(classResult.Items, classBuilder, anyFromClub, input.Config);
                        }
                    }

                    if (anyFromClub)
                    {
                        sb.Append(classBuilder);
                        sb.AppendLine();
                    }
                }

                sb.AppendLine();
            }

            File.WriteAllText(filename, sb.ToString());
        }

        private static bool PrintTeamResults(object[] items, StringBuilder classBuilder, bool anyFromClub, IProvideEventorConfiguration config)
        {
            var resultLines = items.OfType<TeamResult>()
                .Select(x => PrintTeamResult(x, config))
                .Where(x => x.IsValid)
                .OrderBy(x => x.Rank)
                .ToList();

            foreach (var item in resultLines)
            {
                classBuilder.Append(item.Line);
            }

            return resultLines.Any(x => x.IsCurrentClub);
        }

        private class TeamResultOutput
        {
            public int Rank { get; set; }
            public string Line { get; set; }
            public bool IsCurrentClub { get; set; }
            public bool IsValid { get; set; }
        }

        private static readonly Dictionary<string, string> StatusMap = new Dictionary<string, string>
        {
            { "disqualified", "DSQ" },
            { "mispunch", "DSQ" },
            { "didnotfinish", "DNF" },
            { "null", "DNF" },
            { "didnotstart", "DNS" }
        };

        private static TeamResultOutput PrintTeamResult(TeamResult item, IProvideEventorConfiguration config)
        {
            var classBuilder = new StringBuilder();

            Organisation organisation = null;

            if (item.Items != null)
            {
                organisation = item.Items[0] as Organisation;
            }
            else
            {
                if (item.TeamMemberResult.Any())
                    organisation = item.TeamMemberResult.First().Organisation;
            }

            var isOwnClub = false;

            if (organisation.OrganisationId != null)
                isOwnClub = organisation.OrganisationId.Text[0] == config.OrgId;

            var teamstatus = item.TeamStatus ?? new TeamStatus { value = TeamStatusValue.Cancelled };
            var isOk = teamstatus.value == TeamStatusValue.OK;

            // Ikke skriv ut lag fra andre klubber som ikke er godkjente
            if (!isOwnClub && !isOk)
                return new TeamResultOutput { IsValid = false, IsCurrentClub = false };

            var teamMemberResults = item.TeamMemberResult.OrderBy(x => int.Parse(x.Leg));

            var lastRunner = teamMemberResults.Last();
            var teamName = item.TeamName.Text.FirstOrDefault();

            var overallResultPosition = "Ukjent";

            if (lastRunner.OverallResult != null)
            {
                overallResultPosition = lastRunner.OverallResult.TeamStatus.value.ToString();
            
                if (lastRunner.OverallResult.ResultPosition != null)
                {
                    overallResultPosition = (lastRunner.OverallResult.ResultPosition.Text ?? new string[] { }).FirstOrDefault();
                }

                if (overallResultPosition != null && StatusMap.Keys.Any(x => x.ToLower() == overallResultPosition.ToLower()))
                {
                    overallResultPosition = StatusMap[overallResultPosition.ToLower()];
                }
            }

            var runners = teamMemberResults.Select(x => string.Join(" ", x.Person.PersonName.Given.Select(g => (g.Text ?? new[] { "Ingen" }).FirstOrDefault()).ToList()) + " " + (x.Person.PersonName.Family.Text ?? new[] { "løper" }).FirstOrDefault()).ToList();
            classBuilder.AppendFormat("{0}. {1} ({2})", overallResultPosition, teamName, string.Join(", ", runners));
            classBuilder.AppendLine();

            int rank;
            if (!int.TryParse(overallResultPosition, out rank))
            {
                rank = 99999;
            }

            return new TeamResultOutput
            {
                Rank = rank,
                IsCurrentClub = isOwnClub,
                IsValid = true,
                Line = classBuilder.ToString()
            };
        }


        private static bool? PrintPersonResult(PersonResult personResult, StringBuilder classBuilder, bool anyFromClub, Event eventorEvent)
        {
            var config = eventorEvent.Config;

            var person = personResult.Item as Person;

            var club = personResult.Item1 as Organisation;
            var result = personResult.Item2 as Result;

            if (!string.IsNullOrEmpty(eventorEvent.EventRaceId))
            {
                result = personResult.RaceResult.FirstOrDefault(x => ((EventRaceId)x.Item).Text[0] == eventorEvent.EventRaceId)?.Item1 as Result;
            }

            if (result == null)
                return null;

            var name = string.Format("{0} {1}", person.PersonName.Given.FirstOrDefault().Text[0],
                person.PersonName.Family.Text[0]);
            var clubName = club.ShortName?.Text[0] ?? club.Name.Text[0];

            var clubOrgId = string.Empty;

            if (club.OrganisationId != null)
                clubOrgId = club.OrganisationId.Text[0];

            var isCurrentClub = clubOrgId == config.OrgId;

            // Ikke ok resultat (not started, disk etc)
            if (result.ResultPosition == null || result.ResultPosition.Text[0] == "0")
            {
                // Har tid (dvs. startet) og fra egen klubb 
                if (result.Time != null && result.Time.Text[0] != "0:00" && isCurrentClub)
                {
                    var competitorstatus = ShortenCompetitorStatus(result);

                    if (!string.IsNullOrEmpty(competitorstatus))
                        classBuilder.Append(competitorstatus + " ");

                    classBuilder.AppendFormat("{0}", name);

                    //if (!isCurrentClub)
                    classBuilder.AppendFormat(", {0}", clubName);

                    classBuilder.AppendLine();
                    anyFromClub = anyFromClub || isCurrentClub;
                }
            }
            else
            {
                var rank = result.ResultPosition.Text[0];
                classBuilder.AppendFormat("{0}. {1}", rank, name);

                //if (!isCurrentClub)
                classBuilder.AppendFormat(", {0}", clubName);

                classBuilder.AppendLine();

                if (club.OrganisationId != null)
                    anyFromClub = anyFromClub || isCurrentClub;
            }
            return anyFromClub;
        }

        private static string ShortenCompetitorStatus(Result result)
        {
            var status = result.CompetitorStatus.value;

            if (status == CompetitorStatusValue.OK)
                return string.Empty;
            if (status == CompetitorStatusValue.Disqualified)
                return "DSQ";
            if (status == CompetitorStatusValue.DidNotFinish)
                return "DNF";
            if (status == CompetitorStatusValue.DidNotStart)
                return "DNS";

            return status.ToString();
        }

        private static void DownloadResultsFor(Event[] events)
        {
            foreach (var input in events)
            {
                var eventor = new EventorRequest(input.Config);

                var filename = BuildFilename(input.EventId);

                if (File.Exists(filename))
                    continue;

                var uri = string.Format("results/organisation?organisationIds=**ORGID**&eventId={0}&top=1", input.EventId);
                //var uri = string.Format("results/event?eventId={0}&includeSplitTimes=false", input.EventId);
                var resultsXml = eventor.GetXml(uri);
                File.WriteAllText(filename, resultsXml);

                //var startListUri = string.Format("starts/organisation?organisationIds=**ORGID**&eventId={0}", input.EventId);
                //var startListXml = eventor.GetXml(startListUri);
                //File.WriteAllText(BuildFilenameStartList(input.EventId), startListXml);

                //var entriesUri = string.Format("entries?organisationIds=**ORGID**&eventIds={0}&includePersonElement=true&includeEventElement=true", input.EventId);
                //var entriesUri = string.Format("entries?eventIds={0}&includePersonElement=true&includeEventElement=true", input.EventId);
                //var entriesXml = eventor.GetXml(entriesUri);
                //File.WriteAllText(BuildFilenameEntries(input.EventId), entriesXml);

            }
        }

        private static string BuildFilename(int eventId)
        {
            var filename = string.Format(@"c:\eventor\eventresults\{0}.xml", eventId);
            return filename;
        }

        private static string BuildFilenameStartList(int eventId)
        {
            var filename = string.Format(@"c:\eventor\eventresults\{0}_start.xml", eventId);
            return filename;
        }

        private static string BuildFilenameEntries(int eventId)
        {
            var filename = string.Format(@"c:\eventor\eventresults\{0}_entries.xml", eventId);
            return filename;
        }
    }

    internal class Event
    {
        public int EventId { get; set; }
        public string Shortname { get; set; }
        public string EventRaceId { get; set; }
        
        public IProvideEventorConfiguration Config;

        public Event(int eventId, string shortname)
        {
            EventId = eventId;
            Shortname = shortname;
            Config = new AppSettings();
        }
    }

    internal class EventSwe : Event
    {
        public EventSwe(int eventId, string shortname) : base(eventId, shortname)
        {
            Config = new EventorConfigSwe();
        }
    }

    public class EventorConfigSwe : AppSettings, IProvideEventorConfiguration
    {
        public EventorConfigSwe() : base("_SWE")
        {

        }
    }
}
