using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class PostThingsRequest
    {
        public IFormFile? ImageFile { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public DateTime? MissigDate  { get; set; }
        [Required]
        [RegularExpression("01[0125][0-9]{8}", ErrorMessage = "Invalid Phone Number")]
        public string? PhoneNumber  { get; set; }
        [Required]

        public string Town  { get; set; }
        [Required]
        public string City  { get; set; }
        [Required]
        public bool IsSearcher { get; set; }
        [Required]
        public string UserId { get; set; }


    }
}
