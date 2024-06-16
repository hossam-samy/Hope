using Hope.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hope.Core.Features.PostOperation.Commands.CreatePostForPeople
{
    public class CreatePostForPeopleCommand:IRequest<Response>
    {
     
        public int? Age { get; set; }

        public string Condition { get; set; }
        public string? Name { get; set; }
       
        public string Gendre { get; set; }
        public IFormFile? ImageFile { get; set; }
     
        public string Description { get; set; }

        public string? MissigDate { get; set; }

      
        public string? PhoneNumber { get; set; }

        public string Town { get; set; }
        public string City { get; set; }
      
        public bool IsSearcher { get; set; }
    }
    
}
