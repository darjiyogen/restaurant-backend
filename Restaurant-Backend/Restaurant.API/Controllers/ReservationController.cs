using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Interface;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

namespace Restaurant.API.Controllers
{

    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationService _service;

        public ReservationController(ILogger<ReservationController> logger, IReservationService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAsync([FromQuery] DateTimeOffset? Start, [FromQuery] DateTimeOffset? End, [FromQuery] int? Seat)
        {
            var result = await _service.GetAsync(Start, End, Seat);
            return Ok(result);
        }

        [HttpGet]
        [Route("/{ReservationId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int ReservationId)
        {
            var result = await _service.GetByIdAsync(ReservationId);
            if (result != null) {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAsync(ReservationViewModel ReservationVM)
        {
            var result = await _service.CreateAsync(ReservationVM);

            return Ok(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAsync(ReservationViewModel ReservationVM)
        {
            var result = await _service.UpdateAsync(ReservationVM);

            return Ok(result);
        }

        [HttpDelete]
        [Route("/{ReservationId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int ReservationId)
        {
            var result = await _service.DeleteAsync(ReservationId);
            return Ok(result);
        }
    }
}