using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }
        public int Id { get; set; }

        [DisplayName("Order No")]
        public string OrderNo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
