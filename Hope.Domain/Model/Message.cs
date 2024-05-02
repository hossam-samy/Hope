using System.ComponentModel.DataAnnotations.Schema;

namespace Hope.Domain.Model
{
    public class Message:BaseEntity
    {
         
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; }
       
        [ForeignKey("Recipient")]
        public string RecipientId { get; set; }
       public virtual User Recipient { get; set; }
       
        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public virtual User Sender { get; set; }
        public bool IsRead { get; set; }=false;
        public bool IsDeleted { get; set; }=false ;

    }
}
