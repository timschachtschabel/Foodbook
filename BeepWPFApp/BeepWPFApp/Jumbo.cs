using HtmlAgilityPack; //Program heeft HTMLAgilityPack als dependency, installeren via NuGet
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace BeepWPFApp
{
    class Jumbo
    {
        private static async Task<string> Productnaam(string barcode)
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
            //Error handeling, dit betekend dat het product niet gevonden is
            if (title == "Jumbo Groceries")
            {
                return "Product niet gevonden";
            }
            else return title;
        }

        private static async Task<string> Productprijs(string barcode)
        {
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;
            //var url = "https://www.jumbo.com/spa-reine-mineraalwater-koolzuurvrij-75cl/727334FLS/";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("maaktnietuit");
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            string prijsruw = (from x in htmlDocument.DocumentNode.DescendantsAndSelf()
                               where x.Name == "span" && x.Attributes.Contains("class")
                               where x.Attributes["class"].Value == "jum-price-format"
                               select x.InnerText).FirstOrDefault();
            // Error handeling, als de prijs niet gevonden kan worden
            if (prijsruw != null)
            {
                //Formattering
                int count = prijsruw.Count();
                int pos = count - 2;
                string prijs = prijsruw.Insert(pos, ".");

                return prijs;
            }
            else return "Prijs niet gevonden";
        }

        public static string GetProductPrice(string barcode)
        {
            return Task.Run(() => Productprijs(barcode)).Result;
        }
        public static string GetProductName(string barcode)
        {
            return Task.Run(() => Productnaam(barcode)).Result;
        }
    }
}
