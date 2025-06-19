namespace WebApplication1.Request
{
    public class UpdateRoomTypeRequest
    {
        public Guid PropertyId { get; set; }

        public string? Name { get; set; }

        public double DailyPrice { get; set; }

        public string? Currency { get; set; }

        public int MinPersonCount { get; set; }

        public int MaxPersonCount { get; set; }

        public required List<string> Services { get; set; }

        public  required List<string> Amenities { get; set; }
        
    }
}
