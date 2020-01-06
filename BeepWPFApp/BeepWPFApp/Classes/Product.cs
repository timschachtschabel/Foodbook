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
        private  HtmlDocument htmlFile;


        public Product(string barcode)
        {
            htmlFile = GetHtmlDocument(barcode);

            this.barcode = barcode;
            this.Naam = GetProductName(barcode, htmlFile);
            this.Prijs = GetProductprijs(barcode, htmlFile);
            this.PromotiePrijs = GetProductPromotie(barcode, htmlFile);
            this.Allergie = GetAllergie(barcode, htmlFile);
            this.Ingredient = GetIngredient(barcode, htmlFile);
        }

        private HtmlDocument GetHtmlDocument(string barcode)
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

            return htmlDocument;
        }

        private string GetProductName(string barcode, HtmlDocument htmlFileInput)
        {
            Database db = new Database();
            if (!MainWindow.Devmode && db.ProductExist(barcode))
            {
                string resultaat = db.DbGetProductName(barcode);
                return resultaat;
            }
            else
            {
                

                string title = (from x in htmlFileInput.DocumentNode.Descendants()
                    where x.Name.ToLower() == "title"
                    select x.InnerText).FirstOrDefault();
                //Error handeling, dit betekend dat het product niet gevonden is
                if (title == "Jumbo Groceries " || title == null)
                {
                    return "notfound";
                }

                if (!MainWindow.Devmode && caching == false)
                {
                    CacheProduct(barcode);
                }
                return title;
            }
        }

        public override string ToString()
        {
            return Naam;
        }

        private static double GetProductprijs(string barcode, HtmlDocument htmlFileInput)
        {

            string prijsruw = (from x in htmlFileInput.DocumentNode.DescendantsAndSelf()
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

        private static double GetProductPromotie(string barcode, HtmlDocument htmlfileInput)
        {

            string prijsruw = (from x in htmlfileInput.DocumentNode.DescendantsAndSelf()
                where x.Name == "span" && x.Attributes.Contains("class")
                where x.Attributes["class"].Value == "jum-price-format jum-was-price"
                select x.InnerText).FirstOrDefault();
            // Error handeling, als de prijs niet gevonden kan worden

            //test
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

        private static List<string> GetAllergie(string barcode, HtmlDocument htmlFileInput)
        {
            List<string> list = new List<string>();
            

            IEnumerable<string> listItemHtml = htmlFileInput.DocumentNode.SelectNodes(
                    @"//div[@class='jum-product-allergy-info jum-product-info-item col-12']/ul/li")
                ?.Select(li => li.InnerHtml);
            if (listItemHtml != null)
            {
                list = listItemHtml.ToList();
            }


            return list;
        }

        private static List<string> GetIngredient(string barcode, HtmlDocument htmlFileInput)
        {

            IEnumerable<string> listItemHtml = htmlFileInput.DocumentNode.SelectNodes(
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

        private void CacheProduct(string barcode)
        {
            caching = true;
            Database db = new Database();

            string cProductNaam = GetProductName(barcode, htmlFile);
            string cProductPrijs = GetProductprijs(barcode, htmlFile).ToString();

            string cAllergie ="";
            string cIngredient = "";

            List<string> allergienList = GetAllergie(barcode, htmlFile);
            List<string> ingredientList = GetIngredient(barcode, htmlFile);

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