using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Towns> Towns { get; set; }  
    }
}
