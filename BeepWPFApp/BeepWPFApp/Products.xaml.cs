using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            if (finalProductList != null)
            {
                foreach (var testproduct in finalProductList)
                {
                    productlist.Items.Add(testproduct);
                }
            }


        

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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void searchbutton_Click(object sender, RoutedEventArgs e)
        {
            productlist.Items.Clear();

            foreach (var product in finalProductList)
            {
                if (product.naam.ToLower().Contains(searchbox.Text.ToLower()))
                {
                    productlist.Items.Add(product);
                    searchresults.Text = productlist.Items.Count.ToString();
                }
            }
        }

        private void searchbox_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void searchbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchresults.Text = "0";

                productlist.Items.Clear();

                foreach (var product in finalProductList)
                {
                    if (product.naam.ToLower().Contains(searchbox.Text.ToLower()))
                    {
                        productlist.Items.Add(product);
                        searchresults.Text = productlist.Items.Count.ToString();
                    }
                }
            }
            else
            {
                searchresults.Text = "0";

                productlist.Items.Clear();

                foreach (var product in finalProductList)
                {
                    if (product.naam.ToLower().Contains(searchbox.Text.ToLower()))
                    {
                        productlist.Items.Add(product);
                        searchresults.Text = productlist.Items.Count.ToString();
                    }
                }

            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            searchresults.Text = "0";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            api AddShopListItemApi = new api();

            Shoppinglist shoppinglist = (Shoppinglist)shoppinglists.SelectedItem;
            Product product = finalProductList[productlist.SelectedIndex];

            if (AddShopListItemApi.AddShoppinglistItem(shoppinglist.Id, product.Id))
            {
                MessageBox
                    .Show("gelukt");
            }
            else
            {
                MessageBox.Show("nee");
            }

            

        }
    }
}