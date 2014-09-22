using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using WpfApplication2.Backend;

namespace WpfApplication2.Model
{
    internal class ExtractColorPalette
    {
        private List<Color> colorPalette = new List<Color>();
        /// <summary>
        /// Extract Colors From Brush
        /// </summary>
        /// <param name="brush"></param>
        public ExtractColorPalette(LinearGradientBrush brush)
        {
            List<GradientStop> gs = brush.GradientStops.ToList();
            gs.ForEach(ExtractColor);
        }

        public List<Color> ColorPalette
        {
            get
            {
                return colorPalette;
            }
        }

        private void ExtractColor(GradientStop gs)
        {
            colorPalette.Add(gs.Color);
        }
    }
    public class ImageAnalyzerModel: BaseModel
    {
        private string imgFileName;
        private string mouselocation;
        //private Stream imgStream;
        private BitmapImage netImage;
        private LinearGradientBrush lgcolorBar;
        private List<Color> colorPalette;
        private double progressValue;
        
        
        public ImageAnalyzerModel()
        {
            #region ColorBarBrush
            //lgcolorBar = new LinearGradientBrush();

            //colorPalette = new List<Color>();//{ Color.FromRgb(255,255,255), Color.FromRgb(0,0,0)};

            //for (int i = 0; i < 256; ++i)
            //{
            //    colorPalette.Add(Color.FromRgb(0, 0, 0));
            //}

            //lgcolorBar.SpreadMethod = GradientSpreadMethod.Pad;
            //fillColor();

            //int sizecheck = colorPalette.Count;

            //double offset = 0;

            //lgcolorBar.GradientStops.Add(new GradientStop(Colors.Black, offset));
            //offset = 0.0125;
            //lgcolorBar.GradientStops.Add(new GradientStop(Color.FromRgb(0, (18 + 4 + 4), 255), offset));
            //offset = 0.23601778656126504 + (0.975 / 253.0);
            //lgcolorBar.GradientStops.Add(new GradientStop(Color.FromRgb(2, 255, 253), offset));
            //offset = 0.72929841897233283; //+ (0.975 / 253.0);
            //lgcolorBar.GradientStops.Add(new GradientStop(Color.FromRgb(255, 255, 0), offset));
            //offset = 0.987500000000003;
            //lgcolorBar.GradientStops.Add(new GradientStop(Color.FromRgb(255, 0, 0), offset));
            //lgcolorBar.GradientStops.Add(new GradientStop(colorPalette[1], offset));

            //offset = 0.0125;
            //for (int i = 2; i < colorPalette.Count - 1; ++i)
            //{
            //    lgcolorBar.GradientStops.Add(new GradientStop(colorPalette[i], offset));
            //    //Console.Out.WriteLine(offset.ToString());
            //    offset += (0.975 / 253.0);

            //}
            //lgcolorBar.GradientStops.Add(new GradientStop(Colors.Red, offset));

            //IEnumerator clrpa = colorPalette.GetEnumerator();
            //while (clrpa.MoveNext())
            //{
            //    Console.Out.WriteLine("{0:X}\"", clrpa.Current.ToString());
            //}
            //colorPalette[255] = Color.FromRgb(255, 0, 0); // ColorPalette [255]

            //Application.Current
            string value = WpfApplication2.Properties.Resources.GreyBlackHotBrush.ToString();
            try
            {
                lgcolorBar = WpfApplication2.App.Current.FindResource(value) as LinearGradientBrush;
                colorPalette = new ExtractColorPalette(lgcolorBar).ColorPalette;
                int sizecheck = colorPalette.Count;
                // Color gsc = brush.GradientStops[0].Color;
            }
            catch (ResourceReferenceKeyNotFoundException e)
            {
                // Bubble Error - Resource Key Not Founded in Resource Dictionary
                Console.Out.WriteLineAsync("Resource Exception: " + e.Message);
            }
            catch (NullReferenceException)
            {
                //Application is null therefore Resource can not be accessed
                lgcolorBar = new LinearGradientBrush();
            }

           
           

            //colorPalette = ConvertBrushtoColors(lgcolorBar);
            
           

            #endregion
        }

        //private void fillColor()
        //{
        //    //byte i, r, g, b;
        //    //Grey_BlackHot
        //    //for (int k = 0, j = 255; k < 256; ++k, --j)
        //    //{
        //    //    colorPalette[k] = Color.FromRgb((byte)j, (byte)j, (byte)j);
        //    //}
            
        //    //GRey_WhiteHot
        //    //for (int j = 0; j < 256; ++j)
        //    //{
        //    //    colorPalette[j] = Color.FromRgb((byte)j, (byte)j, (byte)j);
        //    //}

        //    //for (i = 0, r = 0, g = 18, b = 255; i < 61; i++, g += 4)
        //    //{
        //    //    colorPalette[i] = Color.FromRgb(r, g, b);
        //    //}

        //    //for (i = 60, r = 0, g = 255, b = 255; i < 188; i++, r += 2, b -= 2)
        //    //{
        //    //    colorPalette[i] = Color.FromRgb(r, g, b);
        //    //}

        //    //for (i = 188, r = 255, g = 255, b = 0; i < 255; i++, g -= 2)
        //    //{
        //    //    colorPalette[i] = Color.FromRgb(r, g, b);
        //    //}


        //    //Fill Common
        //    //r = g = b = 255;
        //    //colorPalette[0] = Color.FromRgb(r, g, b);
        //    //r = 0; g = 0; b = 0;
        //    //colorPalette[1] = Color.FromRgb(r, g, b);
        //    //r = 255;
        //    //colorPalette[255] = Color.FromRgb(r, g, b);


        //}

        public string ImageFileName
        {
            get
            {
                return imgFileName;
            }
            set
            {
                this.imgFileName = value;
                OnPropertyChanged("ImageFileName");
            }

        }

        public string MouseLocation
        {
            get
            {
                return mouselocation;
            }
            set
            {
                this.mouselocation = value;
                OnPropertyChanged("MouseLocation");
            }
        }

        public BitmapImage NetImage
        {
            get
            {
                return netImage;
            }
            set
            {
                this.netImage = value;
                OnPropertyChanged("NetImage");
            }
        }

        public LinearGradientBrush ColorBar
        {
            get
            {
                return lgcolorBar;

            }
            set
            {
                this.lgcolorBar = value;
                OnPropertyChanged("ColorBar");
            }
        }

        public double ProgressValue
        {
            get
            {
                return progressValue;
            }
            set
            {
                this.progressValue = value;
                OnPropertyChanged("ProgressValue");
            }
        }

    }
}
