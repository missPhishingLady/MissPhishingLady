using MissPhishingLady.UrlScan;
using MissPhishingLady.VT;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissPhishingLady
{
    internal class Program
    {
        //debug show virustotal
        static void ShowVTDomainReport(VTDomainReport repo)
        {
            Console.WriteLine(repo.harmless);
            Console.WriteLine(repo.malicious);
            Console.WriteLine(repo.suspicious);
            Console.WriteLine(repo.timeout);
            Console.WriteLine(repo.undetected);
        }

        //debug show urlscan
        static void ShowUrlScanSubmission(Submission submission)
        {
            Console.WriteLine(submission.message);
            Console.WriteLine(submission.uuid);
            Console.WriteLine(submission.result);
            Console.WriteLine(submission.api);
            Console.WriteLine(submission.visibility);
            Console.WriteLine(submission.options_useragent);
            Console.WriteLine(submission.url);
            Console.WriteLine(submission.country);
            Console.WriteLine(submission.screenshot);
            Console.WriteLine(submission.dom);
        }

        static void Main(string[] args)
        {
            RestClient client = new RestClient();
            
            //virustotal api
            VTDomainReport domainRepo = new VTDomainReport();
            VTDomainReportRequest vTDomainReportRequest = new VTDomainReportRequest();
            domainRepo.analyzeUrl = "";
            vTDomainReportRequest.ApiRequest(client, domainRepo);
            ShowVTDomainReport(domainRepo);
            
            //urlscan api
            Submission submission = new Submission();
            SubmissionRequest request = new SubmissionRequest();
            submission.analyzeUrl = "";
            request.ApiRequest(client, submission);
            ShowUrlScanSubmission(submission);
        }
    }
}
