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
using System.Windows.Shapes;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for Favorieten.xaml
    /// </summary>
    public partial class Favorieten : Window
    {
        public Favorieten()
        {
            InitializeComponent();
            listbox2.ItemsSource = lstPage.lstFav;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            favlb1.Items.Add(listbox2.SelectedItem);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            favlb2.Items.Add(listbox2.SelectedItem);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            favlb3.Items.Add(listbox2.SelectedItem);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            listbox2.Items.Add(favlb1.SelectedItem);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            listbox2.Items.Add(favlb2.SelectedItem);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            listbox2.Items.Add(favlb3.SelectedItem);
        }

        private void listbox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void favlb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
