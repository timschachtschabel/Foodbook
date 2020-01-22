using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepWPFApp.Classes
{
    class Shoppinglistproduct
    {
        public int Shoppinglist_Id { get; set; }
        public int User_id { get; set; }
        public int Product_Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
