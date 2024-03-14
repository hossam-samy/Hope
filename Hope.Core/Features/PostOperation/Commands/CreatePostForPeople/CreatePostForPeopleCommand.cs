﻿using Hope.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Hope.Core.Features.PostOperation.Commands.CreatePostForPeople
{
    public class CreatePostForPeopleCommand:IRequest<Response>
    {
        public  string? UserId { get; set; }
     
        public int? Age { get; set; }

        public string Condition { get; set; }
        public string? Name { get; set; }
       
        public string Gendre { get; set; }
        public IFormFile? ImageFile { get; set; }
     
        public string Description { get; set; }

        public DateTime? MissigDate { get; set; }

      
        public string? PhoneNumber { get; set; }

        public string Town { get; set; }
        public string City { get; set; }
      
        public bool IsSearcher { get; set; }
    }
    
}