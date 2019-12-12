using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;



namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for scannerPage.xaml
    /// </summary>
    public partial class scannerPage : Page
    {
        //var aanmaken
        string nummer = null;
        //list moet static anders gaat ie leeg na refresh.
        public  static List<Produkt> ProductenLijst = new List<Produkt>();
      
        public scannerPage()
        {
            InitializeComponent();
            
        }

        public void Page_KeyDown_1(object sender, KeyEventArgs e)
        {
            //laat alleen nummers toe
            if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("I only accept numbers, sorry. :(", "This textbox says...");
                nummer = null;
            }
            //Voeg keycode toe, zet keycode om naar cijfer
            else
            {
                nummer += e.Key;
                int index = nummer.Length - 2;
                nummer = nummer.Remove(index, 1);

                if (nummer.Length == 13)
                {
                    //maak nieuw product aan
                    Produkt nieuwProdukt = new Produkt(Jumbo.GetProductprijs(nummer), Jumbo.GetProductName(nummer), nummer);

                    //voeg product toe aan de lijst
                    ProductenLijst.Add(nieuwProdukt);
                    
                    //push naar lijst
                    lstPrijs.Items.Add(nieuwProdukt.Prijs);
                    lstNaam.Items.Add(nieuwProdukt.Naam);
                }
                if (nummer.Length >= 13)
                {
                    nummer = null;
                }

            }
        }
        
        

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            List<string> waarschuwingen = Jumbo.GetAllergie("8712800006046");

            foreach (var VARIABLE in waarschuwingen)
            {
                lstNaam.Items.Add(VARIABLE);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //clear om daarna toe te voegen, als ik dit niet doe gaat t mis
            lstNaam.Items.Clear();
            lstPrijs.Items.Clear();
            foreach (var produkt in ProductenLijst)
            {
                lstPrijs.Items.Add(produkt.Prijs);
                lstNaam.Items.Add(produkt.Naam);
            }

        }
    }
}