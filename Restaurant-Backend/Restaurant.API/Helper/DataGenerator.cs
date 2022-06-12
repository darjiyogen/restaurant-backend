using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;

namespace Restaurant.API.Helper
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RestaurantDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<RestaurantDbContext>>()))
            {
                // Look for any board games.
                if (!context.RestaurantTables.Any())
                {
                    // Data was not already seeded


                    context.RestaurantTables.AddRange(
                        new RestaurantTable
                        {
                            TableId = 1,
                            Name = "Table 1",
                            Location = "Inner",
                            Seats = 2
                        },
                       new RestaurantTable
                       {
                           TableId = 2,
                           Name = "Table 2",
                           Location = "Inner",
                           Seats = 2
                       }, new RestaurantTable
                       {
                           TableId = 3,
                           Name = "Table 3",
                           Location = "Outer",
                           Seats = 6
                       }, new RestaurantTable
                       {
                           TableId = 4,
                           Name = "Table 4",
                           Location = "Terrace",
                           Seats = 2
                       });
                };

                context.SaveChanges();
            }
        }

    }
}
