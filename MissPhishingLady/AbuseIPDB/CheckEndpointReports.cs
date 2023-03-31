using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissPhishingLady.AbuseIPDB
{
    internal class CheckEndpointReports
    {
        public string reportedAt { get; set; }
        public string comment { get; set; }
        public List<dynamic> categories { get; set; }
        public int reporterId { get; set; }
        public string reporterCountryCode { get; set; }
        public string reporterCountryName { get; set; }

        public CheckEndpointReports()
        {
            this.categories = new List<dynamic>();
        }
    }
}
