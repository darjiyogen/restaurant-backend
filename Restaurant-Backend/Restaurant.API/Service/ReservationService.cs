using Microsoft.EntityFrameworkCore;
using Restaurant.API.Interface;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

namespace Restaurant.API.Service
{
    public class ReservationService : IReservationService
    {
        private readonly RestaurantDbContext _context;

        public ReservationService( RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationViewModel>> GetAsync(DateTimeOffset? Start, DateTimeOffset? End, int? Seat)
        {
            var query = this._context.Reservations.AsQueryable();

            if (Start != null)
            {
                query = query.Where(x => x.StartTime >= Start);
            }
            if (End != null)
            {
                query = query.Where(x => x.EndTime <= End);
            }
            if (Seat != null)
            {
                query = query.Where(x => x.Table.Seats > Seat);
            }

            return await query.Select(x => new ReservationViewModel(x)).ToListAsync();

        }

        public async Task<ReservationViewModel?> GetByIdAsync(int ReservationId)
        {
            var reservation = await _context.Reservations.Where(x => x.Id == ReservationId).FirstOrDefaultAsync();
            if (reservation != null) {
                return new ReservationViewModel(reservation);
            }

            return null;
        }


        public async Task<ReservationViewModel?> CreateAsync(ReservationViewModel ReservationVM)
        {

            // Create Customer first
            // If new customer wants to book
            Customer customer = new Customer();

            RestaurantTable Table = await _context.RestaurantTables.FirstOrDefaultAsync(x => x.TableId == ReservationVM.Table.TableId);
            if (Table == null) {
                return null;
            }

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

            Reservation reservation = new Reservation()
            {
                StartTime = ReservationVM.StartTime,
                EndTime = ReservationVM.EndTime,
                TableId = Table.TableId,
                CustomerId = customer.CustomerId
            };

            _context.Reservations.Add(reservation);

            await _context.SaveChangesAsync();

            return new ReservationViewModel(reservation);
        }


        public async Task<ReservationViewModel?> UpdateAsync(ReservationViewModel ReservationVM)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == ReservationVM.Id);

            if (reservation != null)
            {
                reservation.StartTime = ReservationVM.StartTime;
                reservation.EndTime = ReservationVM.EndTime;
                reservation.TableId = ReservationVM.Table.TableId;

                await _context.SaveChangesAsync();
                return new ReservationViewModel(reservation);
            }

            return null;
        }


        public async Task<bool> DeleteAsync(int ReservationId)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.Id == ReservationId);

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
 
        }

    }
}
