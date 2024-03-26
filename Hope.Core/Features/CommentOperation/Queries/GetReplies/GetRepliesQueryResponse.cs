namespace Hope.Core.Features.CommentOperation.Queries.GetReplies
{
   
    public class GetRepliesQueryResponse
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string UserImage { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }

        public int? CommentCount { get; set; }
        public DateTime Date { get; set; }


    }
}
