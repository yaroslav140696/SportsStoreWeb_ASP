using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class OrderExtension
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int orderextensionID { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int Quantity { get; set; }

        [Required]
        public Product Product { get; set; }
    }
}