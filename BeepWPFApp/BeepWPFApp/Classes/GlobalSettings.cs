using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepWPFApp.Classes
{
    public static class GlobalSettings
    {
        public static int Id { get; set; }
        public static string Naam { get; set; }
        public static string Email { get; set; }
        public static string Pass { get; set; }
        public static List<string> AllergieList { get; set; }

        
        public static bool IsAllergic(Product product)
        {

            if (AllergieList != null && !AllergieList.Any())
            {
                return false;
            }

            var newData = AllergieList.Select(i => i.ToString()).Intersect(product.AllergieList);
            if (newData.Any()) return true;

            return false;
        }
    }
}
