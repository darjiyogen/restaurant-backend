using Restaurant.Models.ViewModels;

namespace Restaurant.API.Interface
{
    public interface ITableService
    {
        public Task<List<RestaurantTableViewModel>> GetAsync();

        public Task<RestaurantTableViewModel?> GetByIdAsync(int TableId);

        public Task<RestaurantTableViewModel?> CreateAsync(RestaurantTableViewModel TableVM);

        public Task<RestaurantTableViewModel?> UpdateAsync(RestaurantTableViewModel TableVM);
        
        public Task<bool> DeleteAsync(int TableId);

    }
}
