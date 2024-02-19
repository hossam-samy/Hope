namespace Hope.Domain.Helpers
{
    public class JWT
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public double DurationInDay { get; set; }
            
    }
}
