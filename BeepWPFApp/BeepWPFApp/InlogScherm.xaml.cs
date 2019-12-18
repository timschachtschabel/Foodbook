﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for InlogScherm.xaml
    /// </summary>
    public partial class InlogScherm : Page
    {

        public InlogScherm()
        {
            InitializeComponent();
            
        }

        public void login(string Name, string Pass)
        {
            Database db = new Database();
            if (db.CheckUser(Name, Pass))
            {
                
                MainWindow.AppWindow.EnableButtons();
                MainWindow.AppWindow.changePage(2);
            }
            else
            {
                MessageBox.Show("Kan gebruiker niet vinden", "Error!", MessageBoxButton.OK);
                txtUser.Clear();
                txtPass.Clear();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) // inlogknop
        {
            login(txtUser.Text,txtPass.Password);
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login(txtUser.Text,txtPass.Password);
            }
        }
    }
}
