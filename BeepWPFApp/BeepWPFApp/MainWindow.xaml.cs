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

        scannerPage scan = new scannerPage();
        betaalScherm betaal = new betaalScherm();
        lstPage lst = new lstPage();

        public MainWindow()
        {
            InitializeComponent();
            main.Content = new scannerPage();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstPrijs_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnScanner_Click(object sender, RoutedEventArgs e)
        {
            main.Content = scan;
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            main.Content = betaal;
        }

        private void BtnList_OnClick(object sender, RoutedEventArgs e)
        {
            main.Content = lst;
        }
    }
}
