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
using BeepWPFApp.Classes;


namespace BeepWPFApp
{

    public partial class lstPage : Page
    {



        public lstPage()
        {

            InitializeComponent();
            for (int i = 0; i < lstNaam.Items.Count; i++) ;


        }

        public void Updatelist()
        {

            api Listapi = new api();

            List<Shoppinglist> userShoppinglists = new List<Shoppinglist>();


            userShoppinglists = Listapi.GetShoppinglists(GlobalSettings.Id);
            if (userShoppinglists != null)
            {
                foreach (var shoppinglist in userShoppinglists)
                {

                    shoppinglists.Items.Add(shoppinglist);
                }
            }

        }

        internal object showDialog()
        {
            throw new NotImplementedException();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.switchPage(7);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            api Shoppinglistapi = new api();


            Shoppinglistapi.CreateShoppinglist(shoppinglistname.Text, GlobalSettings.Id);
            
        }

      
    }
}