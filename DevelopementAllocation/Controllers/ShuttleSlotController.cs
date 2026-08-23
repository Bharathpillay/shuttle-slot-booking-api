using Microsoft.AspNetCore.Mvc;
using DevelopementAllocation.Models;
using DevelopementAllocation.Repository.ShuttleSlot;
using DevelopementAllocation.Services.Email;

namespace DevelopementAllocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShuttleSlotController : ControllerBase
    {
        private readonly IShuttleSlotRepository _repository;
        private readonly IEmailService _emailService;

        public ShuttleSlotController(IShuttleSlotRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        [HttpGet("by-date/{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var entries = await _repository.GetByDateAsync(date);
            return Ok(entries);
        }

        [HttpPost]
        public async Task<IActionResult> Book([FromBody] BookSlotRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.MobileNumber) ||
                string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.MailId) ||
                string.IsNullOrWhiteSpace(request.Slot))
            {
                return BadRequest("All fields are required.");
            }

            var existing = await _repository.GetByDateAsync(request.BookingDate);
            if (existing.Any(e => e.Slot == request.Slot))
            {
                return Conflict("This slot is already booked for the selected date.");
            }

            var entry = new ShuttleSlotEntry
            {
                MobileNumber = request.MobileNumber,
                Name = request.Name,
                MailId = request.MailId,
                Slot = request.Slot
            };

            var id = await _repository.CreateAsync(entry, request.BookingDate);

            try
            {
                await _emailService.SendBookingConfirmationAsync(entry, request.BookingDate);
            }
            catch (Exception ex)
            {
                // booking already succeeded in the DB — don't fail the whole request
                // just because the email didn't go out. Log it instead.
                Console.WriteLine($"Failed to send confirmation email: {ex.Message}");
            }

            return Ok(new { id });
        }
    }

    public class BookSlotRequest
    {
        public string MobileNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string MailId { get; set; } = string.Empty;
        public string Slot { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
    }
}