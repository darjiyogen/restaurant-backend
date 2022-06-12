using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restaurant.Models.ViewModels
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public RestaurantTableViewModel Table { get; set; }

        public CustomerViewModel Customer { get; set; }

        public ReservationViewModel() { 
        
        }

        public ReservationViewModel(Reservation model) { 
            this.Id = model.Id;
            this.StartTime = model.StartTime;
            this.EndTime = model.EndTime;
            this.Table = new RestaurantTableViewModel(model.Table);
            this.Customer = new CustomerViewModel(model.Customer);
        }
    }
}
