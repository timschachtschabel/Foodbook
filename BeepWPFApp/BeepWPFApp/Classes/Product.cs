using HtmlAgilityPack; //Program heeft HTMLAgilityPack als dependency, installeren via NuGet
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Windows;
using BeepWPFApp.Classes;
using Newtonsoft.Json;
using RestSharp;

namespace BeepWPFApp
{
    public class Product
    {
        private static bool caching = false;
        public string barcode { get; set; }
        public string Naam { get; set; }
        public double Prijs { get; set; }

        public List<string> AllergieList { get; set; }
        public List<string> IngredientList { get; set; }
        private HtmlDocument htmlFile;

        public Product(string barcode)
        {
            //product bestaat, check info
            if (CheckProductExist(barcode))
            {
                char[] seperator = ".".ToCharArray();
                var url = "http://localhost:50000/getproduct?barcode=" + barcode;
                var client = new RestClient(url);
                var response = client.Execute(new RestRequest());

                ApiResult kaas = JsonConvert.DeserializeObject<ApiResult>(response.Content);

                this.barcode = barcode;
                Naam = kaas.Naam;
                Prijs = kaas.Prijs;

                AllergieList = kaas.Allergie.Split(seperator).ToList();
                IngredientList = kaas.Ingredient.Split(seperator).ToList();

            }

            //Product bestaat niet, check jumbo website
            else
            {
                htmlFile = GetHtmlDocument(barcode);

                this.barcode = barcode;
                this.Naam = GetProductName(barcode, htmlFile);
                this.Prijs = GetProductprijs(barcode, htmlFile);

                this.AllergieList = GetAllergie(barcode, htmlFile);
                this.IngredientList = GetIngredient(barcode, htmlFile);

                CacheProduct(barcode);
            }
        }

        private bool CheckProductExist(string barcode)
        {
            var url = "http://localhost:50000/getproduct?barcode=" + barcode;
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());

            ApiResult testProduct = JsonConvert.DeserializeObject<ApiResult>(response.Content);


            if (testProduct != null)
            {
                return true;
            }

            return false;
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


        public override string ToString()
        {
            string productname = Naam.Replace("&#39;", "'");
            return productname;
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

            string cAllergie = "";
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