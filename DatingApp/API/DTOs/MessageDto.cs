

using System;

namespace API.DTOs
{
    public class MessageDto
    {
         public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderPhotoName { get; set; }


        public int RecipientId { get; set; }
        public string RecipientUserName { get; set; }
        public string RecipientPhotoName { get; set; }


        public string Content { get; set; }
        public DateTime MassegeSent { get; set; }       
         public DateTime? DataRead { get; set; }
 
    }
}
