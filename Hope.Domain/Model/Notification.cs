using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class Notification:BaseEntity
    {
       
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual List<User> Users { get; set; }   
       



    }
}
