using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class OrderMainPart
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int orderMainPartID { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        [Display(Name = "Время заказа")]
        public DateTime OrderTime { get; set; }

        [Required]
        public List<OrderExtension> OrderExtension { get; set; }

        [Required]
        public ShippingDetails ShippingDetails { get; set; }
    }
}
