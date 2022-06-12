using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class RestaurantTable
    {
        public int TableId { get; set; }

        // Name of table, this is just identifier for billing and other internal purpose
        public string Name { get; set; }

        // Free text for location of table e.g Inner, Outer, Terrace
        // This can be ENUM or another table with FK
        public string Location { get; set; }

        // Nos of seats table has
        public int Seats { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}
