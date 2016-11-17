using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите адрес")]
        [Display(Name = "Строка 1")]
        public string Line1 { get; set; }
        [Display(Name = "Строка 2")]
        public string Line2 { get; set; }
        [Display(Name = "Строка 3")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Введите название города")]
        [Display(Name = "Город")]
        public string City { get; set; }
        [Required(ErrorMessage = "Введите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }
        [Display(Name = "Почтовый индекс")]
        public string Zip { get; set; }
        public bool GiftWrap { get; set; }
    }
}
