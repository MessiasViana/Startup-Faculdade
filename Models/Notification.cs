namespace Startup.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime ScheduledDate { get; set; }
        public bool IsSent { get; set; }
    }
}
