using HtmlAgilityPack; //Program heeft HTMLAgilityPack als dependency, installeren via NuGet
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace BeepWPFApp
{
   public class Product
    {
        public string barcode { get; set; }
        public string Naam { get; }
        public double Prijs { get; }
        public double PromotiePrijs { get; }
        public List<string> Allergie { get; }
        public List<string> Ingredient { get; }

        public Product(string barcode)
        {
            this.barcode = barcode;
            this.Naam = GetProductName(barcode);
            this.Prijs = GetProductprijs(barcode);
            this.PromotiePrijs = GetProductPromotie(barcode);
            this.Allergie = GetAllergie(barcode);
            this.Ingredient = GetIngredient(barcode);
        }



        private static string GetProductName(string barcode)
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
            if (title == "Jumbo Groceries " || title == null)
            {
                return "notfound";
            } 
            return title;
        }

        private static double GetProductprijs(string barcode)
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

        private static double GetProductPromotie(string barcode)
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

        private static List<string> GetAllergie(string barcode)
        {
            List<string> list = new List<string>();
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
                ?.Select(li => li.InnerHtml);
            if (listItemHtml != null)
            {
                list = listItemHtml.ToList();
            }
            

            return list;
        }

        private static List<string> GetIngredient(string barcode)
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
                    @"//div[@class='jum-ingredients-info jum-product-info-item col-12']/ul/li")
                ?.Select(li => li.InnerHtml);

            List<string> list = new List<string>();
            if (listItemHtml != null)
            {
                foreach (var VARIABLE in listItemHtml)
                {
                    string parse = VARIABLE.Replace("</span>", "");
                    parse = parse.Replace("<span class='jum-highlighted-ingredient'>", "");

                    list.Add(parse);
                }
            }

            return list;
        }
    }
}
