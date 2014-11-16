using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Crawler;

namespace EventorResults.Core
{
    public class OrganisationPersonsTransformer
    {
        public List<Person> FromXml(string xml)
        {
            var result = new List<Person>();

            foreach (var person in ToDynamic(xml))
            {
                var p = new Person
                    {
                        Id = Keys.Get<Person>(person.PersonId),
                        PersonId = person.PersonId,
                        LastName = person.PersonName.Family,
                        FirstName = person.PersonName.Given,
                        BirthDate = DateTime.Parse((string) person.BirthDate.Date),
                        Sex = person.sex,
                        ModifyDate = DateTime.Parse(string.Format("{0} {1}", person.ModifyDate.Date, person.ModifyDate.Clock)),
                        LastResultDownloadDate = new DateTime(2011, 01, 01)
                    };

                result.Add(p);
            }

            return result;
        }

        private static dynamic ToDynamic(string xml)
        {
            return new DynamicXml(XDocument.Parse(xml).Root);
        }
    }
}
