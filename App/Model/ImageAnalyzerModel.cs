using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using WpfApplication2.Backend;

namespace WpfApplication2.Model
{
    public class ImageAnalyzerModel: BaseModel
    {
        private string imgFileName;
        private string mouselocation;
        //private Stream imgStream;
        private BitmapImage netImage;
        
		public ImageAnalyzerModel()
        {
		
            #region ColorBarBrush
            lgcolorBar = new LinearGradientBrush();
			
            try
            {
                lgcolorBar = Application.Current.FindResource("GreyBlackHotBrush") as LinearGradientBrush;
                // Color gsc = brush.GradientStops[0].Color;
            }
            catch (ResourceReferenceKeyNotFoundException e)
            {
                Console.Out.WriteLineAsync("Resource Exception: " + e.Message);
            }

            #endregion
        
		}

		
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
        

    }
}
