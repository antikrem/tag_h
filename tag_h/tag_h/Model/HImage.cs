using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace tag_h.Model
{
    /* Stores an entire image
     */
    public class HImage
    {

        public int UUID { get; }

        public bool Deleted { get; }
        
        // Static stream used for background loading
        Stream stream = null;

        // String to location of file 
        public string Location { get; }

        // Underlying image bitmap
        BitmapImage image = null;

        // List of tags
        private List<string> _tags = new List<string>();
        public List<string> Tags
        {
            set
            {
                _tags = value;
                for (int i = 0; i < _tags.Count; i++)
                {
                    _tags[i] = _tags[i].Trim();
                }
            }
            get
            {
                return _tags;
            }
        }

        // Creates a HImage 
        public HImage(int UUID, string location)
        {
            this.UUID = UUID;
            Location = location;
        }

        // Returns if the HImage is loaded onto memory
        public bool isImageLoaded()
        {
            return image != null;
        }

        // Gets underlying bitmap
        public BitmapImage getBitmap()
        {
            return image;
        }

        // Loads internal bitmap from disk to memory
        public void loadBitmap()
        {
            // Open stream to image
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }
            stream = File.OpenRead(Location);

            // Create bitmap image
            image = new BitmapImage();

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = stream;
            image.EndInit();
        }

        // Deloads internal bitmap
        // if no references to the internal bitmap is kept
        public void deloadBitmap()
        {
            image = null;
        }

        public (double, double) Size
        {
            get => (image.PixelWidth, image.PixelHeight);
        }

        public bool IsPhysicalExists()
        {
            return File.Exists(Location);
        }
    }
}
