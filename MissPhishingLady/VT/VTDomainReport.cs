using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissPhishingLady.VT
{
    internal class VTDomainReport
    {
        //last_analysis_stats
        public int harmless { get; set; }
        public int malicious { get; set; }
        public int suspicious { get; set; }
        public int timeout { get; set; }
        public int undetected { get; set; }

        //解析するURL
        public string analyzeUrl { get; set; }

        //メンバを初期化
        public VTDomainReport()
        {
            this.harmless = 0;
            this.malicious = 0;
            this.suspicious = 0;
            this.timeout = 0;
            this.undetected = 0;
        }
    }
}
