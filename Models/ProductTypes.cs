using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Type")]
        public string ProductType { get; set; }
    }
}
