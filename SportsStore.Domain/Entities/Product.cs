
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название товара")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание товара")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите коректную цену товара")]
        public decimal Price { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Введите категорию товара")]
        public string Category { get; set; }

    }
}