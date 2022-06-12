using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

namespace Restaurant.API.Controllers
{

    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly RestaurantDbContext _context;

        public ReservationController(ILogger<ReservationController> logger, RestaurantDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<List<ReservationViewModel>> GetAsync()
        {
            return await this._context.Reservations.Select(x => new ReservationViewModel(x)).ToListAsync();
        }

        [HttpGet]
        [Route("GetReservationById/{ReservationId}")]
        public async Task<ReservationViewModel> GetByIdAsync([FromRoute] int ReservationId)
        {
            return await _context.Reservations.Where(x => x.Id == ReservationId).Select(x => new ReservationViewModel(x)).FirstOrDefaultAsync();
        }

        [HttpPost]
        [Route("Post")]
        public async Task<ReservationViewModel> Create(ReservationViewModel ReservationVM)
        {
            // Create Customer first
            // If new customer wants to book
            Customer customer = new Customer();

            if (ReservationVM.Customer.CustomerId == 0)
            {
                CustomerViewModel CustomerVM = ReservationVM.Customer;

                customer = new Customer()
                {
                    CustomerName = CustomerVM.CustomerName,
                    EmailId = CustomerVM.EmailId,
                    PhoneNumber = CustomerVM.PhoneNumber
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                customer.CustomerId = ReservationVM.Customer.CustomerId;
            }

            _context.Reservations.Add(new Reservation()
            {
                StartTime = ReservationVM.StartTime,
                EndTime = ReservationVM.EndTime,
                TableId = ReservationVM.Table.TableId,
                CustomerId = customer.CustomerId
            });

            await _context.SaveChangesAsync();

            return ReservationVM;
        }

        [HttpPut]
        [Route("Put")]
        public async Task<ReservationViewModel> Update(ReservationViewModel ReservationVM)
        {

            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == ReservationVM.Id);

            if (reservation != null)
            {
                reservation.StartTime = ReservationVM.StartTime;
                reservation.EndTime = ReservationVM.EndTime;
                reservation.TableId = ReservationVM.Table.TableId;

                await _context.SaveChangesAsync();
            }

            return ReservationVM;
        }

        [HttpDelete]
        [Route("Delete/{ReservationId}")]
        public async Task<IActionResult> Delete([FromRoute] int ReservationId)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == ReservationId);

            if (reservation == null)
            {
                return NotFound();
            }
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}