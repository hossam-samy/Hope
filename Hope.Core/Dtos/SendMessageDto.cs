using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class SendMessageDto
    {
        public string Content { get; set; }

        public string RecipientId { get; set; }
    }
}
