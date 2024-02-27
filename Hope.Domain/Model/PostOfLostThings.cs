using System.ComponentModel.DataAnnotations.Schema;

namespace Hope.Domain.Model
{
    public class PostOfLostThings:Post
    {
        public string Type { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual User User { get; set; }
        
        public virtual List<User> HiddenThings { get; set; }
        public virtual List<User> PinnedThings { get; set; }



    }
}
