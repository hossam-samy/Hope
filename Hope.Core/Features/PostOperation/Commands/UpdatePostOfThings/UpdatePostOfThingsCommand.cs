using Hope.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hope.Core.Features.PostOperation.Commands.UpdatePostOfThings
{
    public class UpdatePostOfThingsCommand:IRequest<Response>
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
