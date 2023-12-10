using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace ArchLab6
{
    internal class Scraper
    {
        private HtmlWeb _htmlWeb;
        private static CultureInfo ruCulture = new CultureInfo("ru-RU");

        public Scraper()
        {
            _htmlWeb = new HtmlWeb();
        }
        internal NewsTopic ScrapeNews(string url)
        {
            var newsNode = _htmlWeb.Load(url);
            var tags = getNewsTags(newsNode);
            var title = getNewsTitle(newsNode);
            var time = getNewsTime(newsNode);
            var dateTime = getNewsDate(newsNode).ToDateTime(time).ToUniversalTime();
            var viewsCount = getNewsViews(newsNode);
            var imageSrc = getPreviewImageSrc(newsNode);

            return new NewsTopic
            {
                Title = title,
                CreatedAt = dateTime,
                ViewsCount = viewsCount,
                Tags = tags,
                ImageSource = imageSrc,
            };
        }
        private List<string> getNewsTags(HtmlDocument node) => node.DocumentNode.SelectNodes("//div[@class='page-tags__items']/a")
                .AsEnumerable()
                .Select(x => x.InnerText)
                .ToList();
        private string getNewsTitle(HtmlDocument node) => node.DocumentNode.SelectSingleNode("//h1[@class='page__title']").InnerText;

        private DateOnly getNewsDate(HtmlDocument node) => DateOnly.ParseExact(
                node.DocumentNode.SelectSingleNode("//div[@class='page-post-info__date']").InnerText,
                "d MMMM yyyy",
                ruCulture
            );

        private TimeOnly getNewsTime(HtmlDocument node) => TimeOnly.ParseExact(
                node.DocumentNode.SelectSingleNode("//div[@class='page-post-info__time']").InnerText,
                "HH:mm"
            );

        private int getNewsViews(HtmlDocument node) => int.Parse(
                node.DocumentNode.SelectSingleNode("//div[@class='page-post-info__views']").InnerText
            );

        private string getPreviewImageSrc(HtmlDocument node) => node
            .DocumentNode
            .SelectNodes("//div[@class='page__preview']//img")
            .First()
            .Attributes["data-src"]
            .Value;

    }
}
