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
    /// Interaction logic for InlogScherm.xaml
    /// </summary>
    public partial class InlogScherm : Page
    {

        public InlogScherm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // inlogknop
        {
            string user, pass;
            user = txtUser.Text;
            pass = txtPass.Text;
            if (user == "Perry" && pass == "vogelbekdier")
            {

               
            }
            else
            {
                MessageBox.Show("U heeft een verkeerde gebruikersnaam of wachtwoord ingevoerd.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
