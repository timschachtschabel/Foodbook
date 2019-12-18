using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BeepWPFApp
{
    public static class User
    {
        public static string Naam  { get; set; } = string.Empty;
        public static string Email { get; set; }
        public static string AllergieString { get; set; }
        public static string CreationTime { get; set; }
        private static List<string> AllergieList = new List<string>();


        public static bool  IsAllergic(Product product)
        {
            AllergieList = AllergieString.Split(',').ToList();

            if (!AllergieList.Any())
            {
                return false;
            }

            var newData = AllergieList.Select(i => i.ToString()).Intersect(product.Allergie);
            if (newData.Any()) return true;

            return false;
        }


    }
}

