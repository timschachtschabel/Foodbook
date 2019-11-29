using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Emgu.CV;
using System.Windows.Navigation;
using PQScan;
using System.Windows.Shapes;
using PQScan.BarcodeScanner;

namespace BeepWPFApp
{
    /// <summary>
    /// Interaction logic for scannerPage.xaml
    /// </summary>
    public partial class scannerPage : Page
    {
        public scannerPage()
        {
            InitializeComponent();
        }
        public Bitmap camerafeed()
        {
            VideoCapture capture = new VideoCapture();
            Bitmap image = capture.QueryFrame().Bitmap;
            Bitmap kaas = new Bitmap(" C:/Users/joepv/Pictures/Camera Roll/jot.jpg" );
            return kaas;
        }

        

        public void ScanAllTypeBarcode(Bitmap bmp)
        {
            BarcodeResult[] results = BarCodeScanner.Scan(bmp);
            if (results != null)
            {
                foreach (BarcodeResult result in results)
                {
                    Console.WriteLine(result.BarType.ToString() + "-" + result.Data);
                    picturebox.Source = BitmapToImageSource(bmp);
                    
                }
            }
            else
            {
                MessageBox.Show("Niks");
            }
        }
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScanAllTypeBarcode(camerafeed());
        }
    }
}
