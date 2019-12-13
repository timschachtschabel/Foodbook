using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        public void Page_KeyDown_1(object sender, KeyEventArgs e)
        {
            //laat alleen nummers toe
            if ((!char.IsDigit((char) KeyInterop.VirtualKeyFromKey(e.Key)) & (e.Key != Key.Back)) |
                (e.Key == Key.Space))
            {
                e.Handled = true;
                MessageBox.Show("I only accept numbers, sorry. :(", "This textbox says...");
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
                        lstNaam.Items.Add(nieuwProdukt.Naam);
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
                lstNaam.Items.Add(product.Naam);
            }
        }
    }
}