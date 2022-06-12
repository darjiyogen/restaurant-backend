using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models.ViewModels
{
    public class RestaurantTableViewModel
    {
        public int TableId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Seats { get; set; }

        public RestaurantTableViewModel()
        {

        }

        public RestaurantTableViewModel(RestaurantTable model) { 
            this.TableId = model.TableId;
            this.Name = model.Name;
            this.Location = model.Location;
            this.Seats = model.Seats;
        }
    }
}
