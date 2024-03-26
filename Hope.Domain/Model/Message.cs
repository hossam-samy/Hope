using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class Message:BaseEntity
    {
  
        public string Content { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public string ReciverEmail { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
        
    }
}
