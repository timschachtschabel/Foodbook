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
    
    public partial class lstPage : Page
    {
        public static List<string> lstFav = new List<string>();

        public lstPage()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtBox.Text != "")
            {
                lstNaam.Items.Add(this.txtBox.Text);

            }


        }

        internal object showDialog()
        {
            throw new NotImplementedException();
        }

        public void favorietbutton_Click(object sender, RoutedEventArgs e)
        {
            lstFav.Add(lstNaam.SelectedItem.ToString());


        }

        private void lstNaam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listbox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Favorieten fav = new Favorieten();
            fav.ShowDialog();          
           
        }
    }

        
    }
