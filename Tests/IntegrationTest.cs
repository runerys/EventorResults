using System;
using System.IO;
using Eventor.Integration;
using EventorResults.Core;
using FakeItEasy;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IntegrationTest
    {
        private IProvideEventorConfiguration _config;

        [SetUp]
        public void Setup()
        {
            _config = new AppSettings();
        }


        [Test, Explicit]
        public void WebRequest_TransformToObject_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("results/person?personId=13&fromDate=2012-01-01&toDate=2012-12-31&top=1");

            var personResult = ApiSchemaReader.Deserialize(response);

            //var response = request.Execute<string>("persons/organisations/{OrgId}");


            Assert.IsNotNull(personResult);
        }

        [Test, Explicit]
        public void WebRequest_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("results/person?personId=13&fromDate=2012-01-01&toDate=2012-12-31&top=1");
           
            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test, Explicit]
        public void WebRequest_OrganisationEvents_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("events?organisationIds=245");

            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test, Explicit]
        public void WebRequest_Competitor_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("competitor/13");

            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test, Explicit]
        public void WebRequest_Competitors_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("persons/organisations/245?includeContactDetails=true");

            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test, Explicit]
        public void WebRequest_OrganisationPersons_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("competitors?organisationId=141");

            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test, Explicit]
        public void WebRequest_Events_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("events?eventIds=2295,2296,2654,2636,2634,2627,2639,1983&includeEntryBreaks=true");

            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test, Explicit]
        public void WebRequest_RelayEntry_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("results/organisation?eventId=1175&organisationIds=245&top=1");

            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test, Explicit]
        public void WebRequest_StartList_ShouldYieldResult()
        {
            var request = new EventorRequest(_config);

            var response = request.GetXml("starts/event?eventId=1172");

            //var response = request.Execute<string>("persons/organisations/{OrgId}");

            Assert.IsNotEmpty(response, "Skulle fått svar fra Eventor");
        }

        [Test]
        public void ParseOrgPersons()
        {
            var personsXml = File.ReadAllText(@"C:\Users\rune.rystad\Documents\visual studio 2012\Projects\EventorResults\Tests\XmlResponse\OrganisationPersons.xml");

            var transformer = new OrganisationPersonsTransformer();

            var persons = transformer.FromXml(personsXml);

            Assert.IsNotEmpty(persons, "Skulle parset personer");

        }

        [Test]
        public void ParsePersonResults()
        {
            var personsXml = File.ReadAllText(@"C:\Users\rune.rystad\Documents\visual studio 2012\Projects\EventorResults\Tests\XmlResponse\PersonResultWithWinner.xml");

            var transformer = new PersonResultsTransformer();

            var persons = transformer.FromXml(personsXml);

            Assert.IsNotNull(persons, "Skulle parset resultater");

        }

        [Test, Explicit]
        public void ParseDownloadedPersonResults()
        {
            foreach (var file in Directory.GetFiles(@"c:\eventor\xml\"))
            {
                var personsXml = File.ReadAllText(file);

                var transformer = new PersonResultsTransformer();

                var persons = transformer.FromXml(personsXml);

                Assert.IsNotNull(persons, "Skulle parset resultater");
  
            }


        }

        [Test, Explicit]
        public void ParseDownloadedPersonResultsToSchema()
        {
            foreach (var file in Directory.GetFiles(@"c:\eventor\xml\"))
            {
                var personsXml = File.ReadAllText(file);

                var resultListList = ApiSchemaReader.Deserialize(personsXml);

                Assert.IsNotNull(resultListList, "Skulle parset resultater");

            }


        }
    }
}
