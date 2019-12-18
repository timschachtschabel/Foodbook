using System;
using System.Collections.Generic;
using System.Linq;
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
        public static MainWindow AppWindow;
        public MainWindow()

        {
            InitializeComponent();
            AppWindow = this;
            main.Content = new scannerPage();
            AutoUpdater.Start("http://192.168.178.33/update.xaml");
            main.Content = new InlogScherm();
            AppWindow = this;

            if (User.Naam != String.Empty)
            {
                btnCart.IsEnabled = true;
                btnScanner.IsEnabled = true;
                BtnList.IsEnabled = true;
            }

        }

        public void EnableButtons()
        {
            if (User.Naam != String.Empty)
            {
                
                btnCart.IsEnabled = true;
                btnScanner.IsEnabled = true;
                BtnList.IsEnabled = true;
            }
        }

        public void changePage(int index)
        {
            switch (index)
            {
                case 1:
                    main.Content = betaal;
                    break;
                case 2:
                    main.Content = scan;
                    break;
                case 3:
                    main.Content = lst;
                    break;
            }
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
