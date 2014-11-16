using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Crawler;

namespace EventorResults.Core
{
    public class PersonResultsTransformer
    {
        public PersonResults FromXml(string xml)
        {
            var result = new PersonResults();

            result.ResultLists = new List<ResultList>();

            string personId = string.Empty;

            foreach (var resultList in ToDynamic(xml))
            {

                var e = resultList.Event;

                var eventForm = e.eventForm;

                if (eventForm.ToString().StartsWith("Relay"))
                    continue;

                var cr = resultList.ClassResult;
                var ec = cr.EventClass;



                dynamic winner, own;

                List<dynamic> personResults = new List<dynamic>();

                winner = own = null;

                foreach (var res in cr)
                {
                    if (res.XElementName != "PersonResult")
                        continue;

                    personResults.Add(res);
                }

                if (personResults.Count == 1)
                    own = personResults.First();

                if (personResults.Count == 2)
                {
                    var positionFirst = personResults[0].Result.ResultPosition;
                    var positionSecond = personResults[1].Result.ResultPosition;

                    if (positionFirst != null && positionSecond != null)
                    {
                        if (positionFirst == "1" && positionSecond == "1")
                        {
                            winner = personResults[0];
                            own = personResults[1];
                        }
                        else if (positionFirst == "1" && positionSecond != "1")
                        {
                            winner = personResults[0];
                            own = personResults[1];
                        }
                        else if (positionFirst != "1" && positionSecond == "1")
                        {
                            winner = personResults[1];
                            own = personResults[0];
                        }
                    }
                }



                var r = new ResultList
                {
                    Event = new Event
                        {
                            EventForm = e.eventForm,
                            Name = e.Name,
                            StartDate = DateTime.Parse((string)e.StartDate.Date),
                            FinishDate = DateTime.Parse((string)e.FinishDate.Date),
                            EventId = e.EventId,
                            EventClassificationId = e.EventClassificationId,
                            EventStatusId = e.EventStatusId
                        },
                    ClassResult = new ClassResult
                        {
                            EventClass = new EventClass
                                {
                                    EventClassId = ec.EventClassId,
                                    Name = ec.Name,
                                    ClassShortName = ec.ClassShortName,
                                    EventId = ec.EventId,
                                    NoOfEntries = ec.ClassRaceInfo.noOfEntries != null ? int.Parse((string)ec.ClassRaceInfo.noOfEntries) : 0,
                                    NoOfStarts = ec.ClassRaceInfo.noOfStarts != null ? int.Parse((string)ec.ClassRaceInfo.noOfStarts) : 0,
                                    EventRaceId = ec.ClassRaceInfo.EventRaceId
                                },

                        },
                    DownloadedDate = DateTime.UtcNow
                };

                r.ClassResult.Result = GetResult(own, eventForm);

                if (winner != null)
                {
                    r.ClassResult.Winner = GetResult(winner, eventForm);
                }

                result.ResultLists.Add(r);
            }

            result.PersonId = personId;
            result.Person = Keys.Get<Person>(personId);
            result.Id = Keys.Get<PersonResults>(personId);

            return result;
        }

        private PersonResult GetResult(dynamic own, string eventForm)
        {
            if (own == null)
                return null;

            var pr = new PersonResult
                    {
                        LastName = own.Person.PersonName.Family,
                        FirstName = own.Person.PersonName.Given,
                        Sex = own.Person.sex,
                        PersonId = own.Person.PersonId,
                        PersonRef = Keys.Get<Person>(own.Person.PersonId),
                        BirthDate = own.Person.BirthDate != null ? DateTime.Parse((string)own.Person.BirthDate.Date) : DateTime.MinValue,
                        OrgId = own.Organisation.OrganisationId,
                        OrgName = own.Organisation.Name,
                        OrgShortName = own.Organisation.ShortName
                    };


            if (eventForm == "IndSingleDay")
            {
                pr.ResultTime = own.Result.Time;
                pr.TimeDiff = own.Result.TimeDiff;
                pr.ResultPosition = own.Result.ResultPosition;
                pr.CompetitorStatus = own.Result.CompetitorStatus;
            }

            return null;
        }

        private static dynamic ToDynamic(string xml)
        {
            return new DynamicXml(XDocument.Parse(xml).Root);
        }
    }


    public class PersonResult
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Sex { get; set; }

        public string PersonId { get; set; }

        public string PersonRef { get; set; }

        public DateTime BirthDate { get; set; }

        public string OrgId { get; set; }

        public string OrgName { get; set; }

        public string OrgShortName { get; set; }

        public string ResultTime { get; set; }

        public string TimeDiff { get; set; }

        public string ResultPosition { get; set; }

        public string CompetitorStatus { get; set; }
    }

    public class EventClass
    {
        public string EventClassId { get; set; }

        public string Name { get; set; }

        public string ClassShortName { get; set; }

        public string EventId { get; set; }

        public int NoOfEntries { get; set; }

        public int NoOfStarts { get; set; }

        public string EventRaceId { get; set; }
    }

    public class ClassResult
    {
        public EventClass EventClass { get; set; }

        public PersonResult Result { get; set; }

        public PersonResult Winner { get; set; }
    }

    public class Event
    {
        public string EventForm { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime FinishDate { get; set; }

        public string EventId { get; set; }

        public string EventClassificationId { get; set; }

        public string EventStatusId { get; set; }
    }

    public class ResultList
    {
        public Event Event { get; set; }

        public ClassResult ClassResult { get; set; }

        public DateTime DownloadedDate { get; set; }
    }

    public class PersonResults
    {
        public List<ResultList> ResultLists { get; set; }

        public string Id { get; set; }

        public string Person { get; set; }

        public string PersonId { get; set; }
    }
}
