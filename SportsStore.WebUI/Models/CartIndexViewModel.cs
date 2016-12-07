using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public ICartRepository Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}