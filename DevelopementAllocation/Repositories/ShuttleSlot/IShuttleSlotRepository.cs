


using DevelopementAllocation.Models;

namespace DevelopementAllocation.Repository.ShuttleSlot
{
    public interface IShuttleSlotRepository
    {
        Task<IEnumerable<ShuttleSlotEntry>> GetByDateAsync(DateTime date);
        Task<int> CreateAsync(ShuttleSlotEntry entry, DateTime bookingDate);
    }
}