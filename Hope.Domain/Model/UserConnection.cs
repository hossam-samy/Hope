namespace Hope.Domain.Model
{
    public class UserConnection
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string ConnectionId { get; set; }

    }
}
