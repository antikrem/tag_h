using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;

using Workshell.FileFormats;

namespace tag_h.Model
{
    /* Stores an entire image
     */
    public class HImage : IDisposable
    {

        public int UUID { get; }

        public bool Deleted { get; }
        
        Stream _stream = null;
        private Stream Stream
        {
            get
            {
                if (_stream is null)
                {
                    _stream = File.OpenRead(Location);
                }
                return _stream;
            }
        }

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
            // Create bitmap image
            image = new BitmapImage();

            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = Stream;
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

        public void Dispose()
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }
        }
    }
}
