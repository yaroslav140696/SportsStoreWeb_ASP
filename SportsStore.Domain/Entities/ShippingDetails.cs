using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int shippingDetailsID { get; set; }

        [Required(ErrorMessage = "Введите имя получателя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Введите название населенного пункта")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Введите название улицы, дома, квартиры")]
        [Display(Name = "Название улицы, дома/квартиры")]
        public string Line1 { get; set; }

        [Display(Name = "Облать, район")]
        public string Line2 { get; set; }

        [Display(Name = "Дополнительная строка адреса")]
        public string Line3 { get; set; }

        [Display(Name = "Почтовый индекс")]
        public string Zip { get; set; }

        public bool GiftWrap { get; set; }
    }
}
