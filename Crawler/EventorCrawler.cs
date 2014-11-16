using System;
using System.IO;
using System.Linq;
using System.Threading;
using Eventor.Integration;
using EventorResults.Core;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Database.Server;

namespace Crawler
{
    public class EventorCrawler
    {
        private readonly AppSettings _appSettings;
        private EventorRequest _eventor;
        private string _organisationId;

        public EventorCrawler(AppSettings appSettings)
        {
            _appSettings = appSettings;
            _organisationId = _appSettings.OrgId;
            _eventor = new EventorRequest(_appSettings);
        }

        // for each person in organisation       
        //  if not document exist for person
        //     download person results for 2000-01-01 until today
        //  else
        //     is last date for person > today && !initial crawl
        //         download person results from last date until today

        // decode result to json
        // dump result to RavenDB

        public void Crawl()
        {
            var store = new EmbeddableDocumentStore { DataDirectory = @"c:\eventor\RavenDb\", UseEmbeddedHttpServer = true };
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(store.Configuration.Port);
            store.Initialize();

            Console.WriteLine("Opened RavenDb. Inspect and press enter.");
            Console.ReadLine();

            using (var session = store.OpenSession())
            {
                var config = session.Query<CrawlerSettings>().SingleOrDefault();

                if (config != null &&
                    config.LastCrawl != null &&
                    DateTime.UtcNow.Subtract(config.LastCrawl.Value).TotalDays < 1)
                {
                  //  return;
                }

                if (config == null)
                {
                    config = new CrawlerSettings();
                    session.Store(config);
                }

                //GetOrganisationPersons(session);
                GetPersonResults(session);

                config.LastCrawl = DateTime.UtcNow;

                session.SaveChanges();
            }
        }

        private void GetPersonResults(IDocumentSession session)
        {
            var organisationPersons =
                session.Query<Person>()
                       .Where(p => p.OrganisationId == _organisationId)
                       .Take(9999)
                       .ToList();

            foreach (var person in organisationPersons)
            {
                //if (person.Id != Keys.Get<Person>(13))
                //    continue;

                GetNewResultsForPerson(person, session);
                Thread.Sleep(500);
            }
        }

        private void GetNewResultsForPerson(Person person, IDocumentSession session)
        {
            //if (DateTime.UtcNow.Subtract(person.LastResultDownloadDate).TotalDays < 1)
            //    return;
            
            var fromDate = person.LastResultDownloadDate.ToString("yyyy-MM-dd");
            var toDate = DateTime.UtcNow.ToString("yyyy-MM-dd");

            var uri = string.Format("results/person?personId={0}&fromDate={1}&toDate={2}", person.PersonId, fromDate, toDate);

            var resultsXml = _eventor.GetXml(uri);

            File.WriteAllText(@"c:\eventor\xml\" + person.PersonId + ".xml", resultsXml);
        }

        private void GetOrganisationPersons(IDocumentSession session)
        {
            var orgPersonsTransformer = new OrganisationPersonsTransformer();

            var personsXml = _eventor.GetXml("persons/organisations/{OrgId}");
            var persons = orgPersonsTransformer.FromXml(personsXml);

            // Only add new persons
            var existingPersons = session.Query<Person>().ToList();

            foreach (var p in persons)
            {
                var existing = existingPersons.SingleOrDefault(person => person.Id == p.Id);

                if (existing == null)
                {
                    p.OrganisationId = _organisationId;
                    session.Store(p); 
                }
                else
                {
                    existing.OrganisationId = _organisationId;
                }
               
            }
        }
    }
}
