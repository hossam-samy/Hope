using Microsoft.AspNetCore.Identity;

namespace Hope.Domain.Model
{
    public class User:IdentityUser
    {
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
        public virtual List<PostOfLostThings>  lostThings { get; set; }
        public virtual List<PostOfLostPeople>  lostPeople { get; set; }
        public virtual List<Message> Messages   { get; set; }
        public virtual List<Notification> Notifications   { get; set; }

    }
}
