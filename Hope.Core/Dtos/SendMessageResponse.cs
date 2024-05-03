using Hope.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class SendMessageResponse
    {
        public DateTime Date { get; set; } 
        public string Content { get; set; }

        public string RecipientId { get; set; }
       
        public string SenderId { get; set; }
        public bool IsRead { get; set; } 
    }
}
