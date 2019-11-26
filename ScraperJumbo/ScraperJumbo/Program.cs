using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ScraperJumbo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetProductName("5410013117001"));
            Console.WriteLine(GetProductPrice("5410013117001"));
            Console.WriteLine(GetProductFoto("5410013117001"));

            Console.ReadLine();
        }
        public static async Task<string> Productnaam(string barcode)
        {
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;
            //var url = "https://www.jumbo.com/spa-reine-mineraalwater-koolzuurvrij-75cl/727334FLS/";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("maaktnietuit");
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            string title = (from x in htmlDocument.DocumentNode.Descendants()
                            where x.Name.ToLower() == "title"
                            select x.InnerText).FirstOrDefault();


            return title;

        }
        public static async Task<string> Productprijs(string barcode)
        {
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;
            //var url = "https://www.jumbo.com/spa-reine-mineraalwater-koolzuurvrij-75cl/727334FLS/";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("maaktnietuit");
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            string prijs = (from x in htmlDocument.DocumentNode.Descendants()
                        where x.Name == "span" && x.Attributes.Contains("class")
                        where x.Attributes["class"].Value == "jum-price-format"
                            select x.InnerText).FirstOrDefault();

            return prijs;
        }

        public static async Task<string> ProductFoto(string barcode)
        {
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;
            //var url = "https://www.jumbo.com/spa-reine-mineraalwater-koolzuurvrij-75cl/727334FLS/";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("maaktnietuit");
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            string foto = (from x in htmlDocument.DocumentNode.Descendants()
                            where x.Name == "figure" && x.Attributes.Contains("class")
                            where x.Attributes["class"].Value == "jum-product-image-figure col-12"
                           select x.InnerText).FirstOrDefault();



            return foto;

        }


        public static string GetProductFoto(string barcode)
        {
           
           Task.Run(() => ProductFoto(barcode));
            return null;
        }
        public static string GetProductPrice (string barcode)
        {
            return Task.Run(() => Productprijs(barcode)).Result;
        }
        public static string GetProductName(string barcode)
        {

            return Task.Run(() => Productnaam(barcode)).Result;
        }
    }
}
