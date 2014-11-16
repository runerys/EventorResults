using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crawler;
using EventorResults.Core;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class KeyPatternsTests
    {
        [Test]
        public void CreatePersonKey()
        {
            var key = Keys.Get<Person>("23");

            Assert.AreEqual("persons/23", key, "Genererte feil verdi");
        }

        [Test]
        public void GetIntPartOfKey()
        {
            var id = Keys.GetInt("persons/23");

            Assert.AreEqual(23, id, "Hentet ut feil verdi");
        }
    }
}
