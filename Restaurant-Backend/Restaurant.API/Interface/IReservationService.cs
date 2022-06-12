using Restaurant.Models.ViewModels;

namespace Restaurant.API.Interface
{
    public interface IReservationService
    {
        public Task<List<ReservationViewModel>> GetAsync(DateTimeOffset? Start, DateTimeOffset? End, int? Seat);

        public Task<ReservationViewModel?> GetByIdAsync(int ReservationId);

        public Task<ReservationViewModel?> CreateAsync(ReservationViewModel ReservationVM);

        public Task<ReservationViewModel?> UpdateAsync(ReservationViewModel ReservationVM);
        
        public Task<bool> DeleteAsync(int ReservationId);

    }
}
