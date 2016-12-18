using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class TableHelper
    {
        public static MvcHtmlString TableNames(this HtmlHelper html, string CurrentTable, IEnumerable<string> names, Func<string, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            foreach(var str in names)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                if (CurrentTable == str)
                {
                    li.AddCssClass("active");
                }
                //a.MergeAttribute("data-toggle", "tab");
                a.MergeAttribute("href", pageUrl(str));
                a.InnerHtml = str;
                li.InnerHtml = a.ToString();
                result.Append(li.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}