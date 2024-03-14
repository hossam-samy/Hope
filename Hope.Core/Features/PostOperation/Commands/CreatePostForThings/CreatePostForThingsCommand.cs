using Hope.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Hope.Core.Features.PostOperation.Commands.CreatePostForThings
{
    public class CreatePostForThingsCommand:IRequest<Response>
    {

        public string? UserId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime? MissigDate { get; set; }
        public string? PhoneNumber { get; set; }

        public string Town { get; set; }
        public string City { get; set; }
        public bool IsSearcher { get; set; }
    }
}
