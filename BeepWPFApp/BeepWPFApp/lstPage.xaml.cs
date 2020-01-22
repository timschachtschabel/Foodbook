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
    public partial class lstPage : Page
    {
        public lstPage()
        {
            InitializeComponent();
        }

        public void Updatelist()
        {
            api Listapi = new api();

            List<Shoppinglist> userShoppinglists = new List<Shoppinglist>();


            userShoppinglists = Listapi.GetShoppinglists(GlobalSettings.Id);
            if (userShoppinglists != null)
            {
                foreach (var shoppinglist in userShoppinglists)
                {
                    shoppinglists.Items.Add(shoppinglist);
                }
            }
        }

        public void showProducts()
        {
            api listProductsApi = new api();

            List<Shoppinglistproduct> shoppinglistproducts = new List<Shoppinglistproduct>();

            Shoppinglist shoppinglist = shoppinglists.SelectedItem as Shoppinglist;

            shoppinglistproducts = listProductsApi.GetShoppinglistProducts(GlobalSettings.Id, shoppinglist.Id);

            if (shoppinglistproducts != null)
            {
                foreach (var product in shoppinglistproducts)
                {
                    lstNaam.Items.Add(product);
                }
            }
        }

        internal object showDialog()
        {
            throw new NotImplementedException();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.switchPage(7);
        }


        // Delete single product in shoppinglist
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            api DeleteShoppinglistItemApi = new api();

            Shoppinglist shoppinglist = (Shoppinglist)shoppinglists.SelectedItem;
            Shoppinglistproduct product = (Shoppinglistproduct)lstNaam.SelectedItem;

            if (DeleteShoppinglistItemApi.DeleteShoppinglistItem(shoppinglist.Id, product.Product_Id))
            {
                MessageBox
                    .Show("gelukt");
                shoppinglists.Items.Clear();
                Updatelist();
            }
            else
            {
                MessageBox.Show("nee");
            }

        }


        // Create shoppinglist
    private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            api Shoppinglistapi = new api();


            Shoppinglistapi.CreateShoppinglist(shoppinglistname.Text, GlobalSettings.Id);

            shoppinglists.Items.Clear();
            Updatelist();
        }

        private void shoppinglists_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            lstNaam.Items.Clear();
            showProducts();
        }



        // Delete all items in shoppinglist and delete shoppinglist
        private void deleteList_Click(object sender, RoutedEventArgs e)
        {
            api deleteapi = new api();

            Shoppinglist shoppinglist = (Shoppinglist)shoppinglists.SelectedItem;

            deleteapi.DeleteShoppinglistItems(shoppinglist.Id);

           

            if (deleteapi.DeleteShoppinglist(shoppinglist.Id))
            {
                MessageBox
                    .Show("gelukt");
                shoppinglists.Items.Clear();
                Updatelist();            }
            else
            {
                System.Console.WriteLine(shoppinglist.Id);
                MessageBox.Show("nee");
            }

        }
    }
}