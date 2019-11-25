using System;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;
using System.Web.Http;

using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace JumboAPIecht.Controllers
{
    [Route("code/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        public async Task<string> Productnaam(string barcode)
        {

            //   var barcode = "8723400775393";
            var url = "https://www.jumbo.com/zoeken?SearchTerm=" + barcode;
            //var url = "https://www.jumbo.com/spa-reine-mineraalwater-koolzuurvrij-75cl/727334FLS/";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("kanker");
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            String title = (from x in htmlDocument.DocumentNode.Descendants()
                            where x.Name.ToLower() == "title"
                            select x.InnerText).FirstOrDefault();
            return title;
        }

        public string GetProductByCode (string barcode)
        {
            return Task.Run(() => Productnaam(barcode)).Result;
        }

        [HttpGet("code/{code}")]
        public ActionResult<string> Get(string code)
        {
            string resultaat = GetProductByCode(code);
            if (resultaat == null)
            {
                return NotFound();
            }
            return resultaat;
        }

    }
    
}