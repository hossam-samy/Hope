using System.ComponentModel.DataAnnotations.Schema;

namespace Hope.Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        [ForeignKey("comment")]
        public int  CommentId { get; set; }
        public virtual Comment  comment { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
