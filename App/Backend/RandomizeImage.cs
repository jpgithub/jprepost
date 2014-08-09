using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApplication2.Backend
{
    class RandomizeImage : ImageEventHandler
    {

        private const string relativePath = "../Resources/";
        public void SpawningString()
        {
            Random rnd = new Random();
            ImageEventHandlerEventArgs imgArgs = new ImageEventHandlerEventArgs();

            while (true)
            {
                int Nr = rnd.Next(0, 20);
                switch (Nr)
                {
                    case 2:
                        imgArgs.ImageName = relativePath + "Jellyfish.jpg";
                        break;
                    case 3:
                        imgArgs.ImageName = relativePath + "Koala.jpg";
                        break;
                    case 5:
                        imgArgs.ImageName = relativePath + "Chrysanthemum.jpg";
                        break;
                    case 7:
                        imgArgs.ImageName = relativePath + "Lighthouse.jpg";
                        break;
                    case 11:
                        imgArgs.ImageName = relativePath + "Penguins.jpg";
                        break;
                    case 13:
                        imgArgs.ImageName = relativePath + "Tulips.jpg";
                        break;
                    case 17:
                        imgArgs.ImageName = relativePath + "Desert.jpg";
                        break;
                    case 19:
                        imgArgs.ImageName = relativePath + "Hydrangeas.jpg";
                        break;
                    default:
                        imgArgs.ImageName = relativePath + "Ha3.jpg";
                        break;
                }
                OnSpawnChanged(imgArgs);
                System.Threading.Thread.Sleep(500);
            }
        }

        public void SpawningByteStream()
        {
            ImageEventHandlerEventArgs imgArgs = new ImageEventHandlerEventArgs();
            Random rnd = new Random();
            Bitmap myimg; 
            
            //var assembly = Assembly.GetExecutingAssembly();
            //var assemblyName = assembly.GetName().Name;
            //var stream = assembly.GetManifestResourceStream(assemblyName + "." +"Jelly");
            while (true)
            {
                int Nr = rnd.Next(0, 20);
                switch (Nr)
                {
                    case 2:
                        myimg = WpfApplication2.Properties.Resources.Jellyfish;
                        break;
                    case 3:
                        myimg = WpfApplication2.Properties.Resources.Koala;
                        break;
                    case 5:
                        myimg = WpfApplication2.Properties.Resources.Chrysanthemum;
                        break;
                    case 7:
                        myimg = WpfApplication2.Properties.Resources.Lighthouse;
                        break;
                    case 11:
                        myimg = WpfApplication2.Properties.Resources.Penguins;
                        break;
                    case 13:
                        myimg = WpfApplication2.Properties.Resources.Tulips;
                        break;
                    case 17:
                        myimg = WpfApplication2.Properties.Resources.Desert;
                        break;
                    case 19:
                        myimg = WpfApplication2.Properties.Resources.Hydrangeas;
                        break;
                    default:
                        myimg = WpfApplication2.Properties.Resources.Ha3;
                        break;
                }
            //BitmapImage sendImg = new BitmapImage();
            //sendImg.BeginInit();
            //sendImg.StreamSource = 
            //sendImg.EndInit();
            
                if (myimg != null)
                {
                    MemoryStream ByteStr = new MemoryStream();
                    myimg.Save(ByteStr, System.Drawing.Imaging.ImageFormat.Bmp);
                    if (ByteStr != null)
                    {

                        imgArgs.ImageByteStream = ByteStr.ToArray();
                        OnSpawnChanged(imgArgs);

                    }
                    else
                    {
                        Console.Error.WriteLine("ByteStream Return Null");
                        break;
                    }
                }
                else
                {
                    Console.Error.WriteLine("Image Variable Return Null");
                    break;
                }
                System.Threading.Thread.Sleep(500);
            }
        
        }
    }
}
