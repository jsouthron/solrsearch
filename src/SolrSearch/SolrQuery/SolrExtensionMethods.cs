namespace SolrSearch.SolrQuery
{
    using System;
    using System.Linq;
    using System.Web;

    public static class SolrExtensionMethods
    {
        public static string ToSolrQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }

        public static string ToEncodedString(this string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        public static string ToSolrDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd'T'HH:mm:sss'Z'");
        }
    }
}