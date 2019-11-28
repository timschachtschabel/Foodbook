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
using JumboLibrary;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> productenPrijs = null;
        List<string> productenNaam = null;
        
        public MainWindow()
        {
            InitializeComponent();
            lstPrijs.ItemsSource = productenPrijs;
            lstNaam.ItemsSource = productenNaam;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string naam = Jumbo.GetProductName(txtBox.Text);
            string prijs = Jumbo.GetProductPrice(txtBox.Text);

            lstPrijs.Items.Add(prijs);
            lstNaam.Items.Add(naam);

        }
    }
}
