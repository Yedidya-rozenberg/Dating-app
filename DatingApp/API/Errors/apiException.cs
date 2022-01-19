using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class apiException
    {
        public int statusCode { get; set; }
        public string messege { get; set; }
        public string detiles { get; set; }

        public apiException(int statusCode, string messege = null, string detiles = null)
        {
            this.statusCode = statusCode;
            this.messege = messege;
            this.detiles = detiles;
        }
    }
}