using DevelopementAllocation.Models;

namespace DevelopementAllocation.Services.Email
{
    public interface IEmailService
    {
        Task SendBookingConfirmationAsync(ShuttleSlotEntry entry, DateTime bookingDate);
    }
}