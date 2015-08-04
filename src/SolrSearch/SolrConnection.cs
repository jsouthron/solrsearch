using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolrSearch
{
    public class SolrConnection
    {
        private const string _urlFormat = "{0}://{1}:{2}/{3}";
        public string Url { get; set; }
        public int Port { get; set; }
        public string Core { get; set; }
        public bool UseSsl { get; set; }

        public SolrConnection()
        {
            Url = "localhost";
            Port = 8983;
            Core = "#";
            UseSsl = false;
        }

        public SolrConnection(string url, int port, string core)
        {
            Url = url;
            Port = port; 
            Core = core;
            UseSsl = false;
        }

        public string GetUrl()
        {
            var protocol = UseSsl ? "https" : "http";
            return string.Format(_urlFormat, protocol, Url, Port, Core);
        }
    }
}
