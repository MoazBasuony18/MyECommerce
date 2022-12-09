using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models
{
    public class Products
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }

        [DisplayName("Product Color")]
        public string ProductColor { get; set; }

        [DisplayName("Is Available")]

        public bool IsAvailable { get; set; }
        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }
        public int TagNameId { get; set; }

        [ForeignKey("TagNameId")]
        public TagNames TagNames { get; set; }
    }
}
