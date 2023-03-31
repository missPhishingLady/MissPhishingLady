using MissPhishingLady.Lib;
using MissPhishingLady.UrlScan;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissPhishingLady.AbuseIPDB
{
    internal class CheckEndpointRequest
    {
        //abuseipdb api base url
        private string _baseUrl { get; set; }

        public CheckEndpointRequest()
        {
            this._baseUrl = "https://api.abuseipdb.com/api/v2/check";
        }

        //api request method 
        public void ApiRequest(RestClient client, CheckEndpoint checkEndpoint)
        {
            RestRequest request = new RestRequest(this._baseUrl, Method.Get);

            //request headerの設定
            request.AddHeader("Key", ConfigurationManager.AppSettings.Get("ABUSEIPDBAPI"));
            request.AddHeader("Accept", "application/json");
            request.AddParameter("ipAddress", checkEndpoint.analyzeIP);
            request.AddParameter("maxAgeInDays", "90");
            request.AddParameter("verbose", "");

            //api request 実行
            RestResponse response = client.Execute(request);

            //status code check
            CheckStatusCode checkStatusCode = new CheckStatusCode();
            var check = checkStatusCode.Check(response);
            if (check.Item1 && check.Item2 == 200)
            {
                dynamic jsonContent = JsonConvert.DeserializeObject<dynamic>(response.Content);
                checkEndpoint.ipAddress = jsonContent.data.ipAddress;
                checkEndpoint.isPublic = jsonContent.data.isPublic;
                checkEndpoint.ipVersion = jsonContent.data.ipVersion;
                checkEndpoint.isWhiteListed = jsonContent.data.isWhiteListed;
                checkEndpoint.abuseConfidenceScore = jsonContent.data.abuseConfidenceScore;
                checkEndpoint.countryCode = jsonContent.data.countryCode;
                checkEndpoint.countryName = jsonContent.data.countryName;
                checkEndpoint.usageType = jsonContent.data.usageType;
                checkEndpoint.isp = jsonContent.data.isp;
                checkEndpoint.domain = jsonContent.data.domain;
                checkEndpoint.totalReports = jsonContent.data.totalReports;
                checkEndpoint.numDistinctUsers = jsonContent.data.numDistinctUsers;

                foreach(var hostname in jsonContent.data.hostnames)
                {
                    checkEndpoint.hostnames.Add((string)hostname);
                }

                foreach (var report in jsonContent.data.reports)
                {
                    CheckEndpointReports checkEndpointReports = new CheckEndpointReports();
                    checkEndpointReports.reportedAt = report.reportedAt;
                    checkEndpointReports.comment = report.comment;
                    checkEndpointReports.reporterId = report.reporterId;
                    checkEndpointReports.reporterCountryCode = report.reporterCountryCode;
                    checkEndpointReports.reporterCountryName = report.reporterCountryName;
                    foreach (var category in report.categories)
                    {
                        checkEndpointReports.categories.Add((string)category);
                    }
                    checkEndpoint.reports.Add(checkEndpointReports);
                }
            }
        }
    }
}
