using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class Notification
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public virtual User user { get; set; }



    }
}
