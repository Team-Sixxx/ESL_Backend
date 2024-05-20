namespace ESLBackend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string User { get; set; }

        public int MeetingRoomId { get; set; }
        
    }
}
