using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using AutoUpdaterDotNET;
using BeepWPFApp.Enum;

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
        static DetailsPage details = new DetailsPage();
        public static List<string> allergie = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            main.Content = new scannerPage();
            AutoUpdater.Start("http://192.168.178.33/update.xaml");

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
