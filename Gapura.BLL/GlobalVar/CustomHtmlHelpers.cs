using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gapura.BLL.GlobalVar
{
    public static class CustomHtmlHelpers
    {
        public static string Image(this HtmlHelper helper, string src, string alt)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("alt", alt);

            return tb.ToString(TagRenderMode.SelfClosing);
        }
    }
}