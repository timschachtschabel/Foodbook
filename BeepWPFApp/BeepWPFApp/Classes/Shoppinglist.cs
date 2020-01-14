using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeepWPFApp.Classes
{
    class Shoppinglist
    {
        public int Id { get; set; }
        public string User_id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    
}
