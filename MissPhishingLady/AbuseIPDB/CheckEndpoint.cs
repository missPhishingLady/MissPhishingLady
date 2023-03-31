using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissPhishingLady.AbuseIPDB
{
    internal class CheckEndpoint
    {
        //解析するIPアドレス
        public string analyzeIP { get; set; }
        public string ipAddress { get; set; }
        public bool isPublic { get; set; }
        public int ipVersion { get; set; }
        public bool isWhiteListed { get; set; }
        public int abuseConfidenceScore { get; set; }
        public string countryCode { get; set; }
        public string countryName { get; set; }
        public string usageType { get; set; }
        public string isp { get; set; }
        public string domain { get; set; }
        public List<string> hostnames { get; set; }
        public int totalReports { get; set; }
        public int numDistinctUsers { get; set; }
        public string lastReportedAt { get; set; }
        public List<CheckEndpointReports> reports { get; set; }

        public CheckEndpoint()
        {
            this.hostnames = new List<string>();
            this.reports = new List<CheckEndpointReports>();
        }
    }
}
