using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using BeepWPFApp.Classes;
using BeepWPFApp.Enum;

namespace BeepWPFApp
{
    /// <summary>
    ///     Interaction logic for scannerPage.xaml
    /// </summary>
    public partial class scannerPage : Page
    {
        //list moet static anders gaat ie leeg na refresh.
        public static List<Product> ProductenLijst = new List<Product>();

        //var aanmaken
        private string _nummer;

        public scannerPage()
        {
            InitializeComponent();
        }

        //Capture barcode
        //Get info via barcode
        public void Page_KeyDown_1(object sender, KeyEventArgs e)
        {
            //laat alleen nummers toe
            if ((!char.IsDigit((char) KeyInterop.VirtualKeyFromKey(e.Key)) & (e.Key != Key.Back)) |
                (e.Key == Key.Space))
            {
                e.Handled = true;
                MessageBox.Show("Alleen nummers, sorry. :(", "Error!");
                _nummer = null;
            }
            //Voeg keycode toe, zet keycode om naar cijfer
            else
            {
                _nummer += e.Key;
                int index = _nummer.Length - 2;
                _nummer = _nummer.Remove(index, 1);

                if (_nummer.Length == 13)
                {
                    //maak nieuw product aan
                    Product nieuwProdukt = new Product(_nummer);

                    if (nieuwProdukt.Naam == "notfound")
                    {
                        MessageBox.Show("Product niet gevonden", "error");
                    }
                    else
                    {
                        //voeg product toe aan de lijst
                        ProductenLijst.Add(nieuwProdukt);

                        //push naar lijst
                        lstPrijs.Items.Add(nieuwProdukt.Prijs);
                        if (User.IsAllergic(nieuwProdukt))
                        {
                            lstNaam.Items.Add(new ListBoxItem {Content = nieuwProdukt, Background = Brushes.Red});
                        }
                        else lstNaam.Items.Add(nieuwProdukt);
                    }
                }

                if (_nummer.Length >= 13) _nummer = null;
            }
        }


        private void btnScan_Click(object sender, RoutedEventArgs e)
        {


        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //clear om daarna toe te voegen, als ik dit niet doe gaat t mis
            lstNaam.Items.Clear();
            lstPrijs.Items.Clear();
            foreach (var product in ProductenLijst)
            {
                lstPrijs.Items.Add(product.Prijs);
                lstNaam.Items.Add(product);
            }
        }

        //Krijg product informatie
        private void lstNaam_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product product = lstNaam.SelectedItem as Product;

            //Maak nieuwe window aan
            var mw = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;
            if (product != null)
            {
                DetailsPage detail = new DetailsPage();

                //Check of product null is en of de lijst wel iets bevat
                //Daarna info pushen
                if (product.Ingredient.Any())
                {
                    foreach (var VARIABLE in product.Ingredient)
                    {
                        detail.ingredientenLstBox.Items.Add(VARIABLE);
                    }
                }
                else detail.ingredientenLstBox.Items.Add("Dit product heeft geen ingredienten!");


                if (product.Allergie.Any())
                {
                    foreach (var VARIABLE in product.Allergie)
                    {
                        detail.AllergieLstBox.Items.Add(VARIABLE);
                    }
                }
                else detail.AllergieLstBox.Items.Add("Dit product heeft geen Allergie informatie!");

                //Heb je wel iets geselecteerd?
                if (lstNaam.SelectedItem != null)
                {
                    mw.main.Content = detail;
                }
            }
        }
    }
}