using System.ComponentModel.DataAnnotations.Schema;

namespace Hope.Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        
        public virtual List<Comment>  Comments { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("People")]
        public int? Peopleid { get; set; }
        public virtual PostOfLostPeople?  People{ get; set; }
        [ForeignKey("Things")]
        public int? Thingsid { get; set; }
        public virtual PostOfLostThings? Things { get; set; } 


    }
}
