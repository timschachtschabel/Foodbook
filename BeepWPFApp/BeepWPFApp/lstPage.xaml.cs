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
            favlb1.Items.Add(lstNaam.SelectedItem);
        }

        private void favremove1_Click(object sender, RoutedEventArgs e)
        {
            favlb1.Items.Remove(lstNaam.SelectedItem);
        }

        private void favadd2_Click(object sender, RoutedEventArgs e)
        {
            favlb2.Items.Add(lstNaam.SelectedItem);
        }

        private void favremove2_Click(object sender, RoutedEventArgs e)
        {
            favlb2.Items.Remove(lstNaam.SelectedItem);
        }

        private void favadd3_Click(object sender, RoutedEventArgs e)
        {
            favlb3.Items.Add(lstNaam.SelectedItem);
        }

        private void favremove3_Click(object sender, RoutedEventArgs e)
        {
            favlb1.Items.Remove(lstNaam.SelectedItem);
        }

        private void favall1_Click(object sender, RoutedEventArgs e)
        {
            lstNaam.Items.Add(favlb1.SelectedItem);
        }

        private void favall2_Click(object sender, RoutedEventArgs e)
        {
            lstNaam.Items.Add(favlb2.SelectedItem);
        }

        private void favall3_Click(object sender, RoutedEventArgs e)
        {
            lstNaam.Items.Add(favlb3.SelectedItem);
        }

       
    }

        
    }
