using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for betaalScherm.xaml
    /// </summary>
    public partial class betaalScherm : Page
    {
        public betaalScherm()
        {
            InitializeComponent();
        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<double> prijzen = new List<double>();

            foreach (Product nieuwProdukt in scannerPage.ProductenLijst)
            {
               // prijzen.Add(nieuwProdukt.Prijs);
            }
            double totaalbedrag = prijzen.Sum();
            MessageBox.Show("Uw totaal bedrag is: " + totaalbedrag.ToString(), "totaalbedrag:");
        }

    }
}
