using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeepWPFApp.Classes;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        List<Product> finalProductList = new List<Product>();
        public Products()
        {
            InitializeComponent();


            api Testapi = new api();
            //Product testproduct = Testapi.GetProduct("8710398159458");
            //productlist.Items.Add(testproduct);

            finalProductList = Testapi.GetAllProducts();

            foreach (var testproduct in finalProductList)
            {
                productlist.Items.Add(testproduct);
            }

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void searchbutton_Click(object sender, RoutedEventArgs e)
        {
            productlist.Items.Clear();

            foreach (var product in finalProductList)
            {
                if (product.naam.Contains(searchbox.Text))
                {
                    productlist.Items.Add(product);
                    searchresults.Text = productlist.Items.Count.ToString();
                }
            }
        }

        private void searchbox_KeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}