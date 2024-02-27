using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string ReciverEmail { get; set; }
        //public string SenderEmail { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; } 
        
    }
}
