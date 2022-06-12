using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string EmailId { get; set; }

        public string PhoneNumber { get; set; }

        public CustomerViewModel()
        {

        }

        public CustomerViewModel(Customer model)
        {
            this.CustomerId = model.CustomerId;
            this.CustomerName = model.CustomerName;
            this.EmailId = model.EmailId;
            this.PhoneNumber = model.PhoneNumber;
        }
    }
}
