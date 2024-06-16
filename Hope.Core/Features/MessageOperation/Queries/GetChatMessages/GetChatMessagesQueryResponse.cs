using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.MessageOperation.Queries.GetChatMessages
{
    public class GetChatMessagesQueryResponse
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string Content { get; set; }

        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
