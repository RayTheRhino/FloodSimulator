namespace FloodSimulator.Model
{
    public class Alert
    {
        public string? Id { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public string Serverity { get; set; }
        public string Areas { get; set; }
        public Double Lat {  get; set; }
        public Double Long { get; set; }
    }
}
