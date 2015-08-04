namespace SolrSearch.SolrQuery
{
    public class SolrBoolean
    {
        public static string Exists(string field)
        {
            return string.Format("exists({0})", field);
        }

        public static string If(string expression, string trueValue, string falseValue)
        {
            return string.Format("if({0},{1},{2})", expression, trueValue, falseValue);
        }

        public static string Default(string fieldOrFunction, string value)
        {
            return string.Format("def({0},{1})", fieldOrFunction, value);
        }

        public static string Not(string fieldOrFunction)
        {
            return string.Format("not({0})", fieldOrFunction);
        }

        public static string And(string x, string y)
        {
            return string.Format("and({0},{1})", x, y);
        }

        public static string Or(string x, string y)
        {
            return string.Format("or({0},{1})", x, y);
        }
    }
}