// Controllers/FlightBookingController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DevelopementAllocation.Repository.FlightBooking;

namespace DevelopementAllocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightBookingController : ControllerBase
    {
        private readonly IFreightBookingRepository _repository;

        public FlightBookingController(IFreightBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _repository.GetAllAsync();
            return Ok(users);
        }
    }
}