using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft;
using static System.Net.WebRequestMethods;
using System.Configuration;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using MissPhishingLady.Lib;
using System.Text.RegularExpressions;

namespace MissPhishingLady.VT
{
    internal class VTDomainReportRequest
    {
        //Base URL
        private string _baseUrl { get; set; }

        public VTDomainReportRequest()
        {
            this._baseUrl = "https://www.virustotal.com/api/v3/domains/";
        }

        //Infomation Domain Reports API Request
        public void ApiRequest(RestClient client, VTDomainReport domainReport)
        {
            //Analyze URI
            string host = domainReport.analyzeUrl;
            string pattern = "http";

            //URLにプロトコルスキーマが含まれているかチェック
            //含まれていた場合はホスト部のみを抽出
            if (Regex.IsMatch(domainReport.analyzeUrl, pattern))
            {
                Uri uri = new Uri(domainReport.analyzeUrl);
                host = uri.Host;
            }


            RestRequest request = new RestRequest(this._baseUrl + host, Method.Get);
            request.AddHeader("x-apikey", ConfigurationManager.AppSettings.Get("VTAPIKEY"));
            RestResponse response = client.Execute(request);

            //Console.WriteLine(response.Content);
            //check status code
            CheckStatusCode checkStatusCode = new CheckStatusCode();
            var check = checkStatusCode.Check(response);
            if(check.Item1 && check.Item2 == 200)
            {
                dynamic jsonContent = JsonConvert.DeserializeObject<dynamic>(response.Content);
                domainReport.harmless = (int)jsonContent.data.attributes.last_analysis_stats.harmless;
                domainReport.malicious = (int)jsonContent.data.attributes.last_analysis_stats.malicious;
                domainReport.suspicious = (int)jsonContent.data.attributes.last_analysis_stats.suspicious;
                domainReport.timeout = (int)jsonContent.data.attributes.last_analysis_stats.timeout;
                domainReport.undetected = (int)jsonContent.data.attributes.last_analysis_stats.undetected;
            }
        }
    }
}
