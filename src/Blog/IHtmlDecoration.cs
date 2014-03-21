using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kooboo.CMS.Sites.View;
using System.Text.RegularExpressions;
namespace Blog
{
    public interface IHtmlDecoration
    {
        string Output(string input);
    }

    [Kooboo.CMS.Common.Runtime.Dependency.Dependency(typeof(IHtmlDecoration), Key = "HtmlDecoration")]
    public class HtmlDecoration : IHtmlDecoration
    {
        public string Output(string input)
        {
            if (Page_Context.Current.Initialized)
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(input);
                HtmlNodeCollection aNodes = document.DocumentNode.SelectNodes("//a[@href]");
                if (aNodes != null)
                {
                    var nodes = aNodes.Where(o => !o.GetAttributeValue("class", String.Empty).Contains("third-party"));

                    foreach (HtmlNode aNode in nodes)
                    {
                        string url = aNode.GetAttributeValue("href", String.Empty).Trim();
                        string urlLower = url.ToLower();
                        if (urlLower.StartsWith("/") &&
                            !urlLower.StartsWith("//") &&
                            !urlLower.EndsWith(".zip") &&
                            urlLower != "/")
                        {
                            url += ".html";
                            aNode.SetAttributeValue("href", url);
                        }
                    }
                }
                input = document.DocumentNode.OuterHtml;
            }
            //input = Compress(input);

            return input;
        }

        /// <summary>
        /// 压缩html代码
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string Compress(string text)
        {
            text = Regex.Replace(text, @"<!--\S*?-->", string.Empty);
            text = Regex.Replace(text, "\n", " ");
            text = Regex.Replace(text, @">\s+?<", "><");
            //text = Regex.Replace(text, @"\s{2,}", " ");
            //text = Regex.Replace(text, " {2,}", @"\s");
            //text = Regex.Replace(text, @"\s{2,}", @"\s");

            return text;
        }
    }
}
