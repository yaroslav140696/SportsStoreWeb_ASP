using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class CartLine
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int cartlineID { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Product Product { get; set; }

        [Range(0,100)]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}
