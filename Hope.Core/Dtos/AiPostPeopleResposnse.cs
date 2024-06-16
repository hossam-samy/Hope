using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class AiPostPeopleResposnse
    {
        public int? Age { get; set; }
        public string? Name { get; set; }
        public string Gendre { get; set; }
        public string Description { get; set; }
        public DateTime? MissigDate { get; set; }
        public string Condition { get; set; }
        public string? ImageUrl { get; set; }

        public string? PhoneNumber { get; set; }

        public string UserName { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
    }
}
