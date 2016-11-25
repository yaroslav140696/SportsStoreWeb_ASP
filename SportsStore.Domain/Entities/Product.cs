
using System.Collections.Generic;
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

        [Display(Name = "Количество на складе")]
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Неверное значение для количества на складе")]
        public int QuantityInStock { get; set; }

        //[Display(Name ="Главное изображение")]
        //[Required(ErrorMessage = "Задайте главное изображение)")]
        //public Media Avatar { get; set; }

        //[Display(Name ="Изображения")]
        //public List<Media> Pisctures { get; set; }

    }
}