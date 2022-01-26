using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class apiException
    {
        public int StatusCode { get; set; }
        public string Messege { get; set; }
        public string Detiles { get; set; }

        public apiException(int statusCode, string messege = null, string detiles = null)
        {
            this.StatusCode = statusCode;
            this.Messege = messege;
            this.Detiles = detiles;
        }
    }
}