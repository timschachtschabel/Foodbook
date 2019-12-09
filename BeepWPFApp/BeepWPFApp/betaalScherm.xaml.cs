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
            scannerPage pagina = new scannerPage();
            foreach (Produkt nieuwProdukt in scannerPage.ProductenLijst)
            {
                prijzen.Add(nieuwProdukt.Prijs);
            }
            double totaalbedrag = prijzen.Sum();
            MessageBox.Show(totaalbedrag.ToString(), "totaalbedrag:");
        }



    }
}
