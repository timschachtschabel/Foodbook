using HtmlAgilityPack; //Program heeft HTMLAgilityPack als dependency, installeren via NuGet
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;

namespace BeepWPFApp
{
    public class Product
    {
        private static bool caching = false;
        public string barcode { get; set; }
        public string Naam { get; }
        public double Prijs { get; }
        public double PromotiePrijs { get; }
        public List<string> Allergie { get; }
        public List<string> Ingredient { get; }

        public Product(string barcode)
        {
            public string barcode { get; set; }
            public string Naam { get; }
            public double Prijs { get; }
            public double PromotiePrijs { get; }
            public List<string> Allergie { get; }
            public List<string> Ingredient { get; }


        private static string GetProductName(string barcode)
        {
            Database db = new Database();
            if (db.ProductExist(barcode))
            {
                string resultaat = db.DbGetProductName(barcode);
                return resultaat;
            }
            else
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

                if (caching == false)
                {
                    CacheProduct(barcode);
                }
                return title;
            }
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
                if (title == "Jumbo Groceries " || title == null || title == "Boodschappen | Jumbo Supermarkten ")
                {
                    return "notfound";
                }
                return title;
            }

            public override string ToString()
            {
                return Naam;
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
                string htmlcode;
                var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;

                double echteprijs = Convert.ToDouble(prijs);
                return echteprijs;
            }

        }

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
                list = listItemHtml.ToList();
            }

            return list;
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

        private static void CacheProduct(string barcode)
        {
            caching = true;
            Database db = new Database();
            string cProductNaam = GetProductName(barcode);
            string cProductPrijs = GetProductprijs(barcode).ToString();
            string cAllergie ="";
            string cIngredient = "";
            List<string> allergienList = GetAllergie(barcode);
            List<string> ingredientList = GetIngredient(barcode);

            foreach (var VARIABLE in allergienList)
            {
                string input = VARIABLE.Replace("'", "\\'");
                cAllergie = cAllergie + input + ".";
            }

            foreach (var VARIABLE in ingredientList)
            {
                string input = VARIABLE.Replace("'", "\\'");
                cIngredient = cIngredient + input + ".";
            }

            db.CacheProduct(barcode, cProductNaam, cProductPrijs, cIngredient, cAllergie);
            caching = false;
        }
    }
}
