using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace tag_h
{
    /* Stores an entire image
     */
    class HImage
    {
        // Static stream used for background loading
        Stream stream = null;

        // Relative URI 
        private Uri location;

        // Underlying image bitmap
        BitmapImage image = null;

        // Creates a HImage 
        public HImage(string location)
        {
            this.location = new Uri(location);

            // Open stream to image
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }
            stream = File.OpenRead(location);

            // Create bitmap image
            image = new BitmapImage();

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.DecodePixelHeight = 1080;
            image.StreamSource = stream;
            image.EndInit();
        }

        // Gets underlying bitmap
        public BitmapImage getBitmap()
        {
            return image;
        }

        // Get width of file
        public double getWidth()
        {
            return image.Width;
        }

        // Get height of file
        public double getHeight()
        {
            return image.Height;
        }
    }
}
