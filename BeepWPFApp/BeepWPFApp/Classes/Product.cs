
using System;
using System.Collections.Generic;
using Org.BouncyCastle.Cms;


namespace BeepWPFApp
{
    public class Product
    {
        public string naam { get; set; }
        public double prijs { get; set; }
        public string ingredient { get; set; }
        public string allergie { get; set; }

        public List<string> AllergieList = new List<string>();
        public List<string> IngredientList = new List<string>();

        public override string ToString()
        {
            return naam;
        }
    }
}