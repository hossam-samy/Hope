using Hope.Core.Dtos;
using Hope.Core.Features.CommentOperation.Queries.GetReplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents
{
    public class GetAllPostsOfPeopleQueryResponse
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string? Name { get; set; }
        public string Gendre { get; set; }
        public string Description { get; set; }
        public DateTime? MissigDate { get; set; }
        public string Condition { get; set; }
        public string? ImageUrl { get; set; }

        public string? PhoneNumber { get; set; }


        public string Town { get; set; }
        public string City { get; set; }

        public bool IsSearcher { get; set; }
        public string? UserImage { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }

        public int? CommentCount { get; set; }
        public bool IsPeople { get; set; } = true;
        // public List<CommentResponse> Comments { get; set; }
    }
}
