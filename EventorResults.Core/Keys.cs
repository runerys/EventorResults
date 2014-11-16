using System;

namespace EventorResults.Core
{
    public static class Keys
    {
        public static string Get<T>(object id)
        {
            var typename = typeof (T).Name.ToLower();

            return string.Format("{0}s/{1}", typename, id);
        }

        public static int GetInt(string id)
        {
            return int.Parse(id.Substring(id.LastIndexOf("/", StringComparison.Ordinal) + 1));
        }
    }
}
