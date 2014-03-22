using Kooboo.CMS.Sites.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Kooboo.CMS.Sites.View;
using System.Text.RegularExpressions;
using Kooboo.IO;

namespace Blog
{
    public class GenerateHtmlAttribute : ActionFilterAttribute
    {
        private Regex ignore = new Regex("Account|Contents|Sites|Membership", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            var area = filterContext.RouteData.DataTokens["area"] ?? "";
            if (ignore.IsMatch(area.ToString()))
            {
                return;
            }
            var response = filterContext.RequestContext.HttpContext.Response;
            if (Page_Context.Current.Initialized)
            {
                var page = MenuHelper.Current();
                var virtualPath = filterContext.HttpContext.Request.RawUrl.Split('?', '#')[0].Trim();
                if (virtualPath == "/")
                {
                    virtualPath = "/Index.html";
                }
                else if (!virtualPath.EndsWith(".html"))
                {
                    virtualPath += ".html";
                }


                var filePath = filterContext.HttpContext.Server.MapPath("/html" + virtualPath);
                var dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                filterContext.RequestContext.HttpContext.Response.Filter = new HtmlFilter(response.Filter, response.ContentEncoding, filePath);
            }

        }
    }


}
