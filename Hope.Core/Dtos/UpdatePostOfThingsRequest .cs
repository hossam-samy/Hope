using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class UpdatePostOfThingsRequest
    {
        public int Id { get; set; }
        public string? Type { get; set; }

        public string? Description { get; set; }
        public DateTime? MissigDate { get; set; }
        public IFormFile? Image { get; set; }

        public string? PhoneNumber { get; set; }

        public bool? IsSearcher { get; set; }

        public string? Town { get; set; }
        public string? City { get; set; }

    }
}
