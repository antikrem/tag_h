using System;
using System.IO;

using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;

using Workshell.FileFormats;


namespace tag_h.Core.Model
{
    /* Stores an entire image
     */
    public class HImage : IDisposable
    {

        public int UUID { get; }

        public string Location { get; }

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

        public bool FileExists => File.Exists(Location);

        public FileFormat Format => _stream is null ? FileExists ? FileFormat.Get(Location) : null : FileFormat.Get(Stream);

        public HImage(int UUID, string location)
        {
            this.UUID = UUID;
            Location = location;
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
