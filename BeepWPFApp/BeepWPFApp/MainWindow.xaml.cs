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

        
        scannerPage scan = new scannerPage();
        betaalScherm betaal = new betaalScherm();
        lstPage lst = new lstPage();
        static DetailsPage details = new DetailsPage();
        public static List<string> allergie = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            main.Content = new InlogScherm();

            

            allergie.Add(Allergien.Lactose);
            CreateUser("Joep","Diessen",16,allergie);
        }

        public void CreateUser(string Naam, string Achternaam, int leeftijd, List<string> allergieList)
        {
            User.Voornaam = Naam;
            User.Achternaam = Achternaam;
            User.Leeftijd = leeftijd;
            User.AllergieList = allergieList;
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
