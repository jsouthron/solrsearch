namespace SolrSearch.SolrQuery
{
    public class SolrField
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private double _boost;

        public double Boost
        {
            get { return _boost; }
            set { _boost = value; }
        }

        public SolrField(string field, double boost = 0)
        {
            _name = field;
            _boost = boost;
        }

        public override string ToString()
        {
            return _boost > 0 ? _name + "^" + _boost : _name;
        }
    }
}