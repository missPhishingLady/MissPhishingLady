using MissPhishingLady.Lib;
using MissPhishingLady.VT;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MissPhishingLady.UrlScan
{
    internal class SubmissionRequest
    {
        private string _baseUrl { get; set; }

        public SubmissionRequest()
        {
            this._baseUrl = "https://urlscan.io/api/v1/scan/";
        }

        //urlscan api call
        public void ApiRequest(RestClient client, Submission submission)
        {
            RestRequest request = new RestRequest(this._baseUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("API-Key", ConfigurationManager.AppSettings.Get("URLSCANAPIKEY"));
            var jsonBody = new
            {
                url = submission.analyzeUrl,
                visibility = "private"
            };
            request.AddBody(jsonBody);

            RestResponse response = client.Execute(request);

            CheckStatusCode checkStatusCode = new CheckStatusCode();
            var check = checkStatusCode.Check(response);
            if (check.Item1 && check.Item2 == 200)
            {
                dynamic jsonContent = JsonConvert.DeserializeObject<dynamic>(response.Content);
                submission.message = (string)jsonContent.message;
                submission.uuid = (string)jsonContent.uuid;
                submission.result= (string)jsonContent.result;
                submission.api = (string)jsonContent.api;
                submission.visibility = (string)jsonContent.visibility;
                submission.options_useragent = (string)jsonContent.options.useragent;
                submission.url = (string)jsonContent.url;
                submission.country = (string)jsonContent.country;

                //custom member
                submission.screenshot = "https://urlscan.io/screenshots/" + (string)jsonContent.uuid + ".png";
                submission.dom = "https://urlscan.io/dom/" + (string)jsonContent.uuid;
            }
        }
    }
}
