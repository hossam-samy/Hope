using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hope.Domain.Model
{
   
    public abstract class Post
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;   
        public bool IsDeleted { get; set; }=false;  
        public bool IsFound { get; set; }=false;
        public string Town { get; set; }
        public DateTime MissigDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public bool IsSearcher { get; set; }=true;

      

    }
}
