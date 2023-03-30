using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissPhishingLady.UrlScan
{
    internal class Submission
    {
        public string message { get; set; }
        public string uuid { get; set; }
        public string result { get; set; }
        public string api { get; set; }
        public string visibility { get; set; }
        public string options_useragent { get; set; }
        public string url { get; set; }
        public string country { get; set; }

        //custom member
        public string screenshot { get; set; }
        public string dom { get; set; }

        //解析するURL
        public string analyzeUrl { get; set; }

        public Submission()
        {
            this.message = string.Empty;
            this.uuid = string.Empty;
            this.result = string.Empty;
            this.api = string.Empty;
            this.visibility = string.Empty;
            this.options_useragent = string.Empty;
            this.url = string.Empty;
            this.country = string.Empty;

            this.screenshot = string.Empty;
            this.dom = string.Empty;
        }
    }
}
