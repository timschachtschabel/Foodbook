using HtmlAgilityPack; //Program heeft HTMLAgilityPack als dependency, installeren via NuGet
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;


namespace JumboLibrary
{
    public static class Jumbo
    {
        //Krijgt naam uit de titel van de website
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


            return title;

        }
        //Krijgt prijs vanaf website
        private static async Task<string> Productprijs(string barcode)
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


        //Callt de task en geeft het resultaat terug
        public static string GetProductPrice(string barcode)
        {
            return Task.Run(() => Productprijs(barcode)).Result;
        }

        //Callt de task en geeft het resultaat terug
        public static string GetProductName(string barcode)
        {  
            return Task.Run(() => Productnaam(barcode)).Result;
        }

    }
}
