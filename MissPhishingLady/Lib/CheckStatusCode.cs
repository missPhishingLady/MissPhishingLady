using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissPhishingLady.Lib
{
    internal class CheckStatusCode
    {
        private int _statusCode { get; set; }
        private bool _flag { get; set; }

        public CheckStatusCode()
        {
            this._statusCode= 0;
            this._flag = false;
        }

        public (bool, int) Check(RestResponse res)
        {
            switch (res.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    this._statusCode = (int)res.StatusCode;
                    this._flag = true;
                    break;
                default:
                    break;
            }

            return (this._flag, this._statusCode);
        }
    }
}
