namespace webapp_travel_agency.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Text { get; set; }

        public int? TravelId { get; set; }
        public Travel? Travel { get; set; }

        public Message()
        {

        }
    }
}
