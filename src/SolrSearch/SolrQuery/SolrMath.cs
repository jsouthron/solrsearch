namespace SolrSearch.SolrQuery
{
    using System.Collections.Generic;

    public class SolrMath
    {
        public static string ValueOf(string field)
        {
            return string.Format("field({0})", field);
        }

        public static string Oridinal(string field)
        {
            return string.Format("ord({0})", field);
        }

        public static string ReverseOridinal(string field)
        {
            return string.Format("rord({0})", field);
        }

        public static string Sum(List<string> values)
        {
            return string.Format("sum({0})", string.Join(",", values));
        }

        public static string Subtract(List<string> values)
        {
            return string.Format("sub({0})", string.Join(",", values));
        }

        public static string Product(List<string> values)
        {
            return string.Format("product({0})", string.Join(",", values));
        }

        public static string Divide(List<string> values)
        {
            return string.Format("div({0})", string.Join(",", values));
        }

        public static string Mod(string x, string y)
        {
            return string.Format("mod({0},{1})", x, y);
        }

        public static string Pow(string x, string y)
        {
            return string.Format("pow({0},{1})", x, y);
        }

        public static string Square(string x)
        {
            return string.Format("sqrt({0})", x);
        }

        public static string Absolute(string x)
        {
            return string.Format("abs({0})", x);
        }

        public static string Log(string x)
        {
            return string.Format("log({0})", x);
        }

        public static string Linear(string x, string m, string c)
        {
            return string.Format("linear({0},{1},{2})", x, m, c);
        }

        public static string Reciprocal(string x, string m, string a, string b)
        {
            return string.Format("linear({0},{1},{2},{3})", x, m, a, b);
        }

        public static string MaxOf(string x, string c)
        {
            return string.Format("max({0},{1})", x, c);
        }

        public static string MinOf(string x, string y)
        {
            return string.Format("min({0},{1})", x, y);
        }

        public static string Map(string x, string min, string max, string target, string value)
        {
            return string.Format("map({0},{1},{2},{3},{4},{5})", x, min, max, target, value);
        }
    }
}