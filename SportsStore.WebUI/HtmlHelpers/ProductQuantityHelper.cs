using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace SportsStore.WebUI.HtmlHelpers
{
    public static class ProductQuantityHelper
    {
        public static MvcHtmlString ProductQuantity(this HtmlHelper html, string value, int Quantity, Product product)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder tag = new TagBuilder("input");
            tag.MergeAttribute("value", value);
            tag.AddCssClass("btn");
            tag.AddCssClass("btn-sm");
            tag.AddCssClass("btn-info");
            if(value == "+")
            {
                if (product.QuantityInStock <= Quantity)
                {
                    tag.AddCssClass("disabled");
                    tag.MergeAttribute("type", "button");
                }
                else
                {
                    tag.AddCssClass("active");
                    tag.MergeAttribute("type", "submit");
                }
            }
            else
            {
                if (Quantity == 0)
                {
                    tag.MergeAttribute("type", "button");
                    tag.AddCssClass("disabled");
                }
                else
                {
                    tag.AddCssClass("active");
                    tag.MergeAttribute("type", "submit");
                }
            }
            result.Append(tag.ToString());
            return MvcHtmlString.Create(result.ToString());
        }
    }
}