using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for scannerPage.xaml
    /// </summary>
    public partial class scannerPage : Page
    {
        string nummer = null;
        private string productnaam;
        private string productprijs;

        public scannerPage()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
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
                    productnaam = Jumbo.GetProductName(nummer);
                    productprijs = Jumbo.GetProductPrice(nummer);
                }
                if (nummer.Length >= 13)
                {
                    nummer = null;
                }

            }
        }

    }
}