using System;
namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }
        
        
       public AppUser Sender { get; set; }
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }

         public AppUser Recipient { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUserName { get; set; }


        public string Content { get; set; }
        public DateTime MassegeSent { get; set; } = DateTime.Now;
        // public DateTime? DateAccepted { get; set; }
        public DateTime? DataRead { get; set; }

        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
 
    }
}
