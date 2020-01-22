using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using BeepWPFApp.Classes;
using BeepWPFApp.Enum;
using Newtonsoft.Json;
using RestSharp;

namespace BeepWPFApp
{
    /// <summary>
    ///     Interaction logic for scannerPage.xaml
    /// </summary>
    public partial class scannerPage : Page
    {
        //list moet static anders gaat ie leeg na refresh.
        public static BindingList<Product> ProductenLijst = new BindingList<Product>();
        SerialPort _serialPort = new SerialPort();
        private string code;

        //var aanmaken
        private string _nummer;

        public scannerPage()
        {
            InitializeComponent();
            InitScanner("COM8");
            productListbox.ItemsSource = ProductenLijst;
        }

        private void InitScanner(string name)
        {
            try
            {
                _serialPort.Handshake = Handshake.None;
                _serialPort.PortName = name;
                _serialPort.BaudRate = 19200;
                _serialPort.Parity = Parity.None;
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(AddItem);
                _serialPort.ReadTimeout = 500;
                _serialPort.WriteTimeout = 500;
                _serialPort.Open();

                Thread readThread = new Thread(new ThreadStart(getBarcode));
                readThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Verbind AUB een scanner");
            }
        }


        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            api api = new api();
            ProductenLijst.Add(api.GetProduct("8852018201021"));
        }

        private void getBarcode()
        {
            this.Dispatcher.Invoke(new Action(() => { code = _serialPort.ReadExisting(); }));
        }

        private void AddItem(object sender, SerialDataReceivedEventArgs e)
        {
            api api = new api();
            getBarcode();
            Product nieuwProdukt = api.GetProduct(code);
            if (nieuwProdukt == null)
            {
                MessageBox.Show("Product niet gevonden", code);
                return;
            }

            if (nieuwProdukt.naam == "notfound")
            {
                MessageBox.Show("Product niet gevonden", code);
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    //voeg product toe aan de lijst
                    ProductenLijst.Add(nieuwProdukt);
                });
            }
        }


        private void ProductListbox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product product = productListbox.SelectedItem as Product;

            //Maak nieuwe window aan
            var mw = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;
            if (product != null)
            {
                DetailsPage detail = new DetailsPage();

                //Check of product null is en of de lijst wel iets bevat
                //Daarna info pushen
                if (product.IngredientList.Any())
                {
                    foreach (var VARIABLE in product.IngredientList)
                    {
                        detail.ingredientenLstBox.Items.Add(VARIABLE);
                    }
                }
                else detail.ingredientenLstBox.Items.Add("Dit product heeft geen ingredienten!");


                if (product.AllergieList.Any())
                {
                    foreach (var VARIABLE in product.AllergieList)
                    {
                        detail.AllergieLstBox.Items.Add(VARIABLE);
                    }
                }
                else detail.AllergieLstBox.Items.Add("Dit product heeft geen Allergie informatie!");

                //Heb je wel iets geselecteerd?
                if (productListbox.SelectedItem != null)
                {
                    mw.Main.Content = detail;
                }
            }
        }
    }
}