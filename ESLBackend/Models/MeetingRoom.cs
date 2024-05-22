namespace ESLBackend.models
{
    public class MeetingRoom
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public string? Location { get; set; }
        public string? templateId { get; set; }
        
    }
}
