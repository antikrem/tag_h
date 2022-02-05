using System;
using System.IO;

using Workshell.FileFormats;


namespace tag_h.Core.Model
{
    public class HImage : IDisposable
    {

        public int Id { get; }

        public string Location { get; }

        Stream? _stream = null;
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

        public FileFormat Format => _stream is null ? FileFormat.Get(Location) : FileFormat.Get(Stream);

        public HImage(int id, string location)
        {
            Id = id;
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
