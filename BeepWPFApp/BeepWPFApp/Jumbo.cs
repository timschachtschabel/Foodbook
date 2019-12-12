using HtmlAgilityPack; //Program heeft HTMLAgilityPack als dependency, installeren via NuGet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BeepWPFApp
{
    class Jumbo
    {
        public static string GetProductName(string barcode)
        {
            string htmlcode;
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;
            using (WebClient wc = new WebClient())
            {
                wc.Headers["User-Agent"] = "maaktnietuit";
                htmlcode = wc.DownloadString(url);
            }
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlcode);

            string title = (from x in htmlDocument.DocumentNode.Descendants()
                            where x.Name.ToLower() == "title"
                            select x.InnerText).FirstOrDefault();
            //Error handeling, dit betekend dat het product niet gevonden is
            if (title == "Jumbo Groceries" || title == null) 
            {
                return "Product niet gevonden";
            }
            else return title;
        }

        public static double GetProductprijs(string barcode)
        {
            string htmlcode;
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;
            using (WebClient wc = new WebClient())
            {
                wc.Headers["User-Agent"] = "maaktnietuit";
                htmlcode = wc.DownloadString(url);
            }

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlcode);

            string prijsruw = (from x in htmlDocument.DocumentNode.DescendantsAndSelf()
                where x.Name == "span" && x.Attributes.Contains("class")
                where x.Attributes["class"].Value == "jum-price-format"
                select x.InnerText).FirstOrDefault();
            // Error handeling, als de prijs niet gevonden kan worden
            if (prijsruw == null) return 0.0;
            else
            {
                //Formattering
                var count = prijsruw.Count();
                var pos = count - 2;
                var prijs = prijsruw.Insert(pos, ".");

                double echteprijs = Convert.ToDouble(prijs);
                return echteprijs;
            }
        }

        public static double GetProductPromotie(string barcode)
        {
            string htmlcode;
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;

            using (WebClient wc = new WebClient())
            {
                wc.Headers["User-Agent"] = "maaktnietuit";
                htmlcode = wc.DownloadString(url);
            }
            ;

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlcode);

            string prijsruw = (from x in htmlDocument.DocumentNode.DescendantsAndSelf()
                where x.Name == "span" && x.Attributes.Contains("class")
                where x.Attributes["class"].Value == "jum-price-format jum-was-price"
                               select x.InnerText).FirstOrDefault();
            // Error handeling, als de prijs niet gevonden kan worden
            if (prijsruw == null) return 0.0;
            else
            {
                //Formattering
                var count = prijsruw.Count();
                var pos = count - 2;
                var prijs = prijsruw.Insert(pos, ".");

                double echteprijs = Convert.ToDouble(prijs);
                return echteprijs;
            }

        }

        public static List<string> GetAllergie(string barcode)
        {
            string htmlcode;
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;

            // var url = "https://www.jumbo.com/jumbo-witte-bollen-10-stuks/300211STK/";
            using (WebClient wc = new WebClient())
            {
                wc.Headers["User-Agent"] = "maaktnietuit";
                htmlcode = wc.DownloadString(url);
            }

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlcode);

            IEnumerable<string> listItemHtml = htmlDocument.DocumentNode.SelectNodes(
                    @"//div[@class='jum-product-allergy-info jum-product-info-item col-12']/ul/li")
                .Select(li => li.InnerHtml);

            List<string> list = listItemHtml.ToList();

            return list;
        }
    }
}
