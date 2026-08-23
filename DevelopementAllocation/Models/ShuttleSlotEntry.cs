namespace DevelopementAllocation.Models
{
    public class ShuttleSlotEntry
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string MailId { get; set; } = string.Empty;
        public string Slot { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}