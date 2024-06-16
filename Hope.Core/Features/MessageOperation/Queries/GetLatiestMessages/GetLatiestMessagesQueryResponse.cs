namespace Hope.Core.Features.MessageOperation.Queries.GetLatiestMessages
{
    public class GetLatiestMessagesQueryResponse
    {
        public string? DisplayName { get; set; }
        public string? UserImage { get; set; }

        public string RecipentId { get; set; }
        public int Id { get; set; }
        public string  Content{ get; set; }
        public DateTime  Date{ get; set; }
        public bool  IsRead{ get; set; }


    }
}
