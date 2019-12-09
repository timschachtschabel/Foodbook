using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeepWPFApp
{
    public class Produkt
    {
        public double Prijs { get; set; }
        public string Naam { get; set; }
        public string Barcode { get; set; }

        public Produkt(double prijs, string naam, string barcode)
        {
            this.Prijs = prijs;
            this.Naam = naam;
            this.Barcode = barcode;
        }
    }
}
