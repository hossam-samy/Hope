namespace Hope.Core.Features.CommentOperation.Queries.GetReplies
{
    public class GetRepliesQueryResponse
    {
        public List<CommentResponse> Comments { get; set; }
    }
    public class CommentResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }


    }
}
