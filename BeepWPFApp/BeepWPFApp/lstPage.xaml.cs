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



        public lstPage()
        {

            InitializeComponent();
            for (int i = 0; i < lstNaam.Items.Count; i++) ;


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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow.AppWindow.switchPage(7);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}