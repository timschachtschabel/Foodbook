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
using System.Windows.Shapes;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for betaalschermpie.xaml
    /// </summary>
    public partial class betaalschermpie : Window
    {

        public betaalschermpie()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
//            List<double> prijzen = new List<double>();
//            scannerPage pagina = new scannerPage();
//            foreach (Produkt nieuwProdukt in ProductenLijst)
//            {
//                prijzen.Add(nieuwProdukt.Prijs);
//            }
//
//            double totaalbedrag = prijzen.Sum();
//            MessageBox.Show(totaalbedrag.ToString(), "totaalbedrag:");

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            MainWindow list = new MainWindow();
            this.Content = list;
        }

        private void btnScanner_Click(object sender, RoutedEventArgs e)
        {
            scannerPage scanner = new scannerPage();
            this.Content = scanner;
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
