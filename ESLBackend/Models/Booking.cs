namespace ESLBackend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public string User { get; set; }
    }
}
