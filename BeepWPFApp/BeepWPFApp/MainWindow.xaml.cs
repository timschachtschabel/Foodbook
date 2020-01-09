using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using BeepWPFApp.Enum;


namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool Devmode = false;
        public static MainWindow AppWindow;

        static scannerPage scan = new scannerPage();
        static betaalScherm betaal = new betaalScherm();
        static lstPage lst = new lstPage();
        static DetailsPage details = new DetailsPage();
        static Registreer registerpage = new Registreer();
        static InlogScherm login = new InlogScherm();
        static Products productpage = new Products();


        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;

            main.Content = new InlogScherm();

            if (Devmode)
            {
                btnCart.IsEnabled = true;
                btnScanner.IsEnabled = true;
                BtnList.IsEnabled = true;
            }
            

        }

        public void switchPage(int pageIndex)
        {
            switch (pageIndex)
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
                case 4:
                    main.Content = details;
                    break;
                case 5:
                    main.Content = registerpage;
                    break;
                case 6:
                    main.Content = login;
                    break;
                case 7:
                    main.Content = productpage;
                    break;

            }
        }


        private void btnScanner_Click(object sender, RoutedEventArgs e)
        {
            switchPage(2);
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            switchPage(1);
        }

        private void BtnList_OnClick(object sender, RoutedEventArgs e)
        {
            switchPage(3);
        }

        public void EnableButtons()
        {
            btnCart.IsEnabled = true;
            btnScanner.IsEnabled = true;
            BtnList.IsEnabled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
