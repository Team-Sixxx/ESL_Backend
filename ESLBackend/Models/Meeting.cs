namespace ESLBackend.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public int roomid { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string User { get; set; }
    }
}
