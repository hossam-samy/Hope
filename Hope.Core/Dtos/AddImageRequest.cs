using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public class AddImageRequest
    {
        public string UserId { get; set; }
        public IFormFile Image { get; set; }    
    }
}
