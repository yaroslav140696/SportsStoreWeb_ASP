using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class WishListLine
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int WishlistlineID { get; private set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}
