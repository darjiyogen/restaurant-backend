using Microsoft.EntityFrameworkCore;
using Restaurant.API.Interface;
using Restaurant.Data;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

namespace Restaurant.API.Service
{
    public class TableService : ITableService
    {
        private readonly RestaurantDbContext _context;

        public TableService(RestaurantDbContext context)
        {
            _context = context;
        }

        async Task<List<RestaurantTableViewModel>> ITableService.GetAsync()
        {
            return await _context.RestaurantTables.Select(x => new RestaurantTableViewModel(x)).ToListAsync();
        }

        async Task<RestaurantTableViewModel?> ITableService.GetByIdAsync(int TableId)
        {
            return await _context.RestaurantTables.Where(x => x.TableId == TableId).Select(x => new RestaurantTableViewModel(x)).FirstOrDefaultAsync();
        }

        async Task<RestaurantTableViewModel?> ITableService.CreateAsync(RestaurantTableViewModel TableVM)
        {

            RestaurantTable table = new RestaurantTable()
            {
              Location = TableVM.Location,
              Name = TableVM.Name,
              Seats = TableVM.Seats,              
            };

            _context.RestaurantTables.Add(table);

            await _context.SaveChangesAsync();

            return new RestaurantTableViewModel(table);
        }

        // TODO
        async Task<bool> ITableService.DeleteAsync(int TableId)
        {
            throw new NotImplementedException();
        }

        // TODO
        async Task<RestaurantTableViewModel?> ITableService.UpdateAsync(RestaurantTableViewModel TableVM)
        {
            throw new NotImplementedException();
        }
    }
}
