using Hope.Core.Features.CommentOperation.Queries.GetReplies;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfThings
{
    public class GetAllPostsOfThingsQueryResponse
    {
        public int Id { get; set; }
        public string Type { get; set; }

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
        public DateTime CreationDate { get; set; }
        public List<CommentResponse> Comments { get; set; }
    }
}
