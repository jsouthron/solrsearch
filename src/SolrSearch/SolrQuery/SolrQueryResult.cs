namespace SolrSearch.SolrQuery
{
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    public class SolrQueryResult<T>
    {
        public SolrQueryResponseHeader SolrQueryResponseHeader { get; set; }
        public SolrQueryResponse<T> SolrQueryResponse { get; set; }
        public SolrQueryError SolrQueryError { get; set; }
        public IDictionary<string, JObject> Highlighting { get; set; } 
    }

    public class SolrQueryResponse<T>
    {
        public int NumFound { get; set; }
        public int Start { get; set; }
        public IEnumerable<T> Docs { get; set; }
    }

    public class SolrQueryResponseHeader
    {
        public int Status { get; set; }
        public int QTime { get; set; }
        public JObject Params { get; set; }
    }

    public class SolrQueryError
    {
        public string Msg { get; set; }
        public string Code { get; set; }
    }

}
