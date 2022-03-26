namespace API.helpers
{
    public class MessageParams : PaginationParams
    {
        public string Container { get; set; } = "unread";
        public string UserName { get; set; }        
        
    }
}