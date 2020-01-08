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
using BeepWPFApp.Enum;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for Registreer.xaml
    /// </summary>
    public partial class Registreer : Page
    {
        public Registreer()
        {
            InitializeComponent();
        }

        private void RegisterUser(string naam, string wachtwoord, string email, List<string> allergie)
        {
            Database db = new Database();

            db.CreateUser(naam, wachtwoord, email, allergie);

            MainWindow.AppWindow.switchPage(6);

        }

        private void register_click(object sender, RoutedEventArgs e)
        {
            List<string> checkedAllergies = new List<string>();

            if (check1.IsChecked == true)
            {
                checkedAllergies.Add(Allergien.Lactose);
            }
            if (check2.IsChecked == true)
            {
                checkedAllergies.Add(Allergien.Ei);
            }
            if (check3.IsChecked == true)
            {
                checkedAllergies.Add(Allergien.Lupine);
            }
            if (check4.IsChecked == true)
            {
                checkedAllergies.Add(Allergien.Gluten);
            }
            if (check5.IsChecked == true)
            {
                checkedAllergies.Add(Allergien.Noten);
            }
            if (check6.IsChecked == true)
            {
                checkedAllergies.Add(Allergien.Soja);
            }
            if (check7.IsChecked == true)
            {
                checkedAllergies.Add(Allergien.Sesam);

            }

            RegisterUser(RegisterUsername.Text, RegisterPass.Password, RegisterMail.Text, checkedAllergies);

        }
    }
}
