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

        public string Location { get; }

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

        public FileFormat Format => _stream is null ? FileFormat.Get(Location) : FileFormat.Get(Stream);

        public ulong? _hash;
        // Null if hashing not supported
        public ulong? Hash
        {
            get
            {
                if (HImageFormat.IsHashableFormat(this) && _hash is null) 
                {
                    // TODO: On SaveImageQuery, run GenerateHash on seperate task
                    GenerateHash();  
                }
                return _hash;

            }            
        }

        private void GenerateHash()
        {
            if (_stream is null)
            {
                using (var stream = File.OpenRead(Location))
                {
                    _hash = (new AverageHash()).Hash(stream);
                }
            }
            else
            {
                _hash = (new AverageHash()).Hash(_stream);
            }
        }

        // Underlying image bitmap
        BitmapImage image = null;

        public TagSet Tags { get; }


        //public HImage(int UUID, string location)
        //{
        //    this.UUID = UUID;
        //    Location = location;
        //}

        public HImage(int UUID, string location, ulong? hash, TagSet tags )
        //    : this(UUID, location)
        {
            this.UUID = UUID;
            Location = location;
            _hash = hash;
            tags = Tags;
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
