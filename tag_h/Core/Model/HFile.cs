using System;
using System.IO;

using Workshell.FileFormats;
using tag_h.Persistence.Model;
using tag_h.Persistence;

namespace tag_h.Core.Model
{

    public class HFile : IDisposable
    {
        private readonly IDatabase _database;
        private HFileState _state;
        private readonly Lazy<Stream> _stream;

        public int Id => _state.Id;
        public string Location => _state.Location;
        
        public bool FileExists => File.Exists(Location);
        public Stream Stream => _stream.Value;
        public FileFormat Format
            => _stream.IsValueCreated ? FileFormat.Get(Stream) : FileFormat.Get(Location);

        public HFile(IDatabase database, HFileState state)
        {
            _state = state;
            _database = database;
            _stream = new Lazy<Stream>(() => File.OpenRead(Location));
        }

        public void Dispose()
        {
            if (_stream.IsValueCreated)
                _stream.Value.Dispose();
        }

        public void Save()
        {
            _database.SaveFile(_state);
        }

        public void Delete()
        {
            _database.SaveFile(_state);
        }
    }

    public interface IHFileFactory
    {
        HFile Create(HFileState state);
    }

    public class HFileFactory : IHFileFactory
    {
        private readonly IDatabase _database;

        public HFileFactory(IDatabase database)
        {
            _database = database;
        }

        public HFile Create(HFileState state)
            => new(_database, state);
    }
}
