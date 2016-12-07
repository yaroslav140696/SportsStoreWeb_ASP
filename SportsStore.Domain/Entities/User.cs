using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Domain.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int userID { get; private set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirtsName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [DataType(DataType.EmailAddress)]
        [StringLength(450)]
        [Index(IsUnique = true)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        public List<WishListLine> WishList { get; set; }

        public List<CartLine> ShoppingCart { get; set; }

        public List<OrderMainPart> Orders { get; set; }
    }

    public class CurrentUser 
    {
        public User Data { get; set; }
        public bool isAdmin { get; set; }
    }
}
