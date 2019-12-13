using System.Windows;


namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        scannerPage scan = new scannerPage();
        betaalScherm betaal = new betaalScherm();
        lstPage lst = new lstPage();

        public MainWindow()
        {
            InitializeComponent();
            main.Content = new InlogScherm();

            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void lstPrijs_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void btnScanner_Click(object sender, RoutedEventArgs e)
        {
            main.Content = scan;
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            main.Content = betaal;
        }

        private void BtnList_OnClick(object sender, RoutedEventArgs e)
        {
            main.Content = lst;
        }
    }
}
