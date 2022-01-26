using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extantions
{
    public static class DataTimeExtantions
    {
        public static int CalculateAge (this DateTime dob)
        {
            var Age = DateTime.Now.Year-dob.Year;
            if(dob.AddYears(Age) > DateTime.Now){Age--;}
            return Age;

        }
    }
}