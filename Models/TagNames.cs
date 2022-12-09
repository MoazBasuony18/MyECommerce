using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyECommerce.Models
{
    public class TagNames
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Tag Name")]
        public string TagName { get; set; }
    }
}
