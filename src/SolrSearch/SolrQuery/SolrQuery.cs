using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearch.SolrQuery
{
    public class SolrQuery
    {

        private string _version = "version=2.2";
        private string _defaultOperation = "/select?";

        //Replaced for a better synonym Searcher"defType=dismax";
        private string _deftype = "defType=synonym_edismax"; 
        private int _start = 0;
        private int _rows = 10;
        private string _wt = "json";

        private string _synonyms = "synonyms=true";

        //private string[] _escapeChars = ["+", "-", "&&", "||", "!", "(", ")", "{", "}", "[", "]", "^",  "~", "*", "?", ":"];

        private readonly List<SolrField> _qfields = new List<SolrField>();

        private readonly List<string> _returnFields = new List<string>();

        private List<string> _queryTerms { get; set; }

        public bool IsScored { get; set; }
        public bool IsFaceted { get; set; }

        public SolrQuery(int start = 0, int rows = 100, string wt = "json")
        {
            _queryTerms = new List<string> {_version, _synonyms, _deftype};

            _start = start;
            _rows = rows;
            _wt = wt;

            _returnFields.Add("*");
        }

        public SolrQuery Skip(int start) { _start = start; return this; }
        public SolrQuery Take(int rows) { _rows = rows; return this; }
        public SolrQuery ResponseFormat(string wt) { _wt = wt; return this; }

        public SolrQuery SearchFor(string term = "*")
        {
            _queryTerms.Add("q=" + term);

            return this;
        }

        public SolrQuery SearchFuzzy(string term = "*", double proximity = .7)
        {
            _queryTerms.Add("q=" + term.ToLower() + "~" + proximity);

            return this;
        }

        public SolrQuery InFields(List<SolrField> fields)
        {
            fields.ForEach(f => _qfields.Add(f));

            return this;
        }

        public SolrQuery In(string field, List<string> values)
        {
            string q = "fq=" + field + ":(" + string.Join(" ", values) + ")";
            _queryTerms.Add(q);

            return this;
        }

        public SolrQuery FilterQuery(string filter)
        {
            _queryTerms.Add("fq=" + filter);
            return this;
        }

        public SolrQuery NegativeFilterQuery(IEnumerable<string> filterList, IEnumerable<SolrField> fieldList)
        {
            foreach (var curr in fieldList.Select(x => x.Name))
            {
                _queryTerms.Add("fq=-" + curr + ":(" + String.Join(" OR ", filterList) + ")");
            }
            return this;
        }

        public SolrQuery AfterDate(DateTime after, string field)
        {
            _queryTerms.Add("fq=" + field + ":[" + after.ToSolrDate() + "+TO+*]");
            return this;
        }

        public SolrQuery BeforeDate(DateTime before, string field)
        {
            _queryTerms.Add("fq=" + field + ":[0001-01-01T00:00:00Z+TO+" + before.ToSolrDate() + "]");
            return this;
        }

        public SolrQuery BetweenDates(DateTime start, DateTime end, string field)
        {
            _queryTerms.Add("fq=" + field + ":[" + start.ToSolrDate() + "+TO+" + end.ToSolrDate() + "]");
            return this;
        }

        public SolrQuery Range(object start, object end, string field)
        {
            _queryTerms.Add("fq=" + field + ":[" + start.ToString() + "+TO+" + end.ToString() + "]");
            return this;
        }

        public SolrQuery GeoFilter(string field, double latitude, double longitude, double proximity)
        {
            string fq = "fq={!geofilt sfield=" + field + "}";
            fq += "&pt=" + latitude + "," + longitude;
            fq += "&d=" + proximity;

            _queryTerms.Add(fq);

            return this;
        }

        public SolrQuery GroupBy(string field, string sortBy = "score desc", int limit = 1, bool flatten = true)
        {
            string term = "group=true&group.field=" + field;
            term += flatten ? "&group.main=true" : "";
            term += "&group.sort=" + sortBy;
            term += "&group.limit=" + limit;
            _queryTerms.Add(term);

            return this;
        }

        public SolrQuery ReturnFields(List<string> fields)
        {
            fields.Remove("*");
            fields.ForEach(field => _returnFields.Add(field));

            return this;
        }

        public SolrQuery ScoreDocs()
        {
            this.IsScored = true;
            _returnFields.Add("score");

            return this;
        }

        public SolrQuery FacetOn(List<string> fields)
        {
            this.IsFaceted = true;

            string term = "facet=true&facet.mincount=1&facet.field=";
            term += string.Join("&facet.field=", fields);
            _queryTerms.Add(term);

            return this;
        }

        public SolrQuery ShowMoreLike(string field)
        {
            _queryTerms.Add("mlt=true&mlt.fl=" + field);

            return this;
        }

        public SolrQuery BoostNewDocuments(string field = "CreatedOn")
        {
            _queryTerms.Add("bf=recip(abs(ms(NOW/DAY," + field + ")),1,6.3E10,6.3E10)");

            return this;
        }

        public SolrQuery BoostFun(string fieldOrFunction)
        {
            _queryTerms.Add("bf=" + fieldOrFunction);

            return this;
        }

        public string ToQueryString()
        {
            _queryTerms.Add("wt=" + _wt);
            _queryTerms.Add("start=" + _start);
            _queryTerms.Add("rows=" + _rows);

            var qfields = _qfields.Select(f => f.ToString());
            _queryTerms.Add("qf=" + string.Join(" ", qfields));

            _queryTerms.Add("fl=" + string.Join(",", _returnFields));

            return _defaultOperation + string.Join("&", _queryTerms);
        }
    }
}
