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
        private byte[] byteStream;

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
/*
        public Stream ImageStream
        {
            get
            {
                return imgStream; 
            }
            set
            {
                this.imgStream = value;
                OnPropertyChanged("ImageStream");
            }

        }
*/
        

    }
}
