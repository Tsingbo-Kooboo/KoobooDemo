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
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var response = filterContext.RequestContext.HttpContext.Response;

            if (Page_Context.Current.Initialized)
            {
                var page = MenuHelper.Current();
                var virtualPath = page.VirtualPath;
                if (page.IsDefault)
                {
                    virtualPath = "/Index.html";
                }
                else if (!virtualPath.EndsWith(".html"))
                {
                    virtualPath += ".html";
                }
                var filePath = filterContext.HttpContext.Server.MapPath(virtualPath);

                filterContext.RequestContext.HttpContext.Response.Filter = new HtmlFilter(response.Filter, response.ContentEncoding, filePath);
            }

            base.OnResultExecuted(filterContext);
        }
    }


}
