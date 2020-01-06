using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using BeepWPFApp;
using HtmlAgilityPack;

namespace WebaPi1
{
    public class GetProductController : ApiController
    {
        [HttpGet]
        public IHttpActionResult yeet(string barcode)
        {
           Database db = new Database();
           var product=  db.dbGetproductInfo(barcode);

           if (product != null)
           {
               return Json(new
               {
                   Naam = product.Naam, barcode = product.barcode, Prijs = product.Prijs, Allergie = product.Allergie,
                   Ingredient = product.Ingredient
               });
           } 
           return NotFound();
        }
    }
}