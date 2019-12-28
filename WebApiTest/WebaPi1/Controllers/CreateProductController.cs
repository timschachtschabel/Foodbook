using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BeepWPFApp;

namespace WebaPi1
{
    public class CreateProductController : ApiController
    {
        public bool get(string barcode, string naam)
        {
            Database db = new Database();
            return db.CreateProduct(barcode, naam);
        }
    }
}