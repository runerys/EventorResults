using System;

namespace Crawler
{
    public class Person
    {
        public string Id { get; set; }

        public string PersonId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Sex { get; set; }

        public DateTime ModifyDate { get; set; }

        public DateTime LastResultDownloadDate { get; set; }        

        public string OrganisationId { get; set; }
    }
}