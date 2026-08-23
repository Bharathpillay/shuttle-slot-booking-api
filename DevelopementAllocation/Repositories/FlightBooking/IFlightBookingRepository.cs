// Repository/FlightBooking/IFreightBookingRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using DevelopementAllocation.Models;

namespace DevelopementAllocation.Repository.FlightBooking
{
    public interface IFreightBookingRepository
    {
        Task<IEnumerable<TestUser>> GetAllAsync();
    }
}