namespace Hope.Domain.Model
{
    public class Cities
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Towns> Towns { get; set; }  
    }
}
