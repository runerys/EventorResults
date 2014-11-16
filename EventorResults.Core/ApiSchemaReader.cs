using System.IO;
using System.Xml.Serialization;
using Eventor.Schema;

namespace EventorResults.Core
{
    public class ApiSchemaReader
    {
        private static XmlSerializer _serializer = new XmlSerializer(typeof(ResultListList));

        public static ResultListList Deserialize(string xml)
        {
            using (var sr = new StringReader(xml))
            {
                return (ResultListList)_serializer.Deserialize(sr);
            }
        }

        public static T ReadAs<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var sr = new StringReader(xml))
            {
                return (T)serializer.Deserialize(sr);
            }
        }
    }
}
