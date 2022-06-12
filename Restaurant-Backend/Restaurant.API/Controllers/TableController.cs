using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.API.Interface;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

namespace Restaurant.API.Controllers
{

    [ApiController]
    [Route("table")]
    public class TableController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly ITableService _service;

        public TableController(ILogger<ReservationController> logger, ITableService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _service.GetAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("/{TableId}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int TableId)
        {
            var result = await _service.GetByIdAsync(TableId);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAsync(RestaurantTableViewModel TableVM)
        {
            var result = await _service.CreateAsync(TableVM);

            return Ok(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAsync(RestaurantTableViewModel TableVM)
        {
            var result = await _service.UpdateAsync(TableVM);

            return Ok(result);
        }

        [HttpDelete]
        [Route("/{TableId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int TableId)
        {
            var result = await _service.DeleteAsync(TableId);
            return Ok(result);
        }

    }
}