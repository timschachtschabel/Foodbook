﻿using System;
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


        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;

            main.Content = new InlogScherm();

            if (Devmode)
            {
                Database db = new Database();
                btnCart.IsEnabled = true;
                btnScanner.IsEnabled = true;
                BtnList.IsEnabled = true;

                main.Content = scan;
                try
                {
                    db.CheckUser("joep", "123");

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
            }
        }

        public void EnableButtons()
        {
            btnCart.IsEnabled = true;
            btnScanner.IsEnabled = true;
            BtnList.IsEnabled = true;  
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Database db = new Database();

            if (!db.OpenConnection())
            {
                MessageBox.Show("Kan geen verbinding maken met de Database server!", "Error!");
            }
        }
    }
}
