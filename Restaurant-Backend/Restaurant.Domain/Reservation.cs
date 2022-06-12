using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public virtual RestaurantTable Table { get; set; }

        public int TableId { get; set; }

        public virtual Customer Customer { get; set; }

        public int CustomerId { get; set; }

    }
}
