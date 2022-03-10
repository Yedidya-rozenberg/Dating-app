using System;
namespace API.helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;

        public int PageNumber { get; set; }
        public string CorrentUser { get; set; }
        public string Gender { get; set; }

        private int _pageSize = 10;
        public int PageSize
        {
            get=>_pageSize;
            set=> _pageSize =  Math.Min(MaxPageSize,value);
        }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 150;   

        public string OrderBy { get; set; }
               
    }
}