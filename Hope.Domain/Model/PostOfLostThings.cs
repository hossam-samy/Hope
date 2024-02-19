namespace Hope.Domain.Model
{
    public class PostOfLostThings:Post
    {
        public string Type { get; set; }
        public string UserId { get; set; }
        public virtual User   user { get; set; }

        
    }
}
