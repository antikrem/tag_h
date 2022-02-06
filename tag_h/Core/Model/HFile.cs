using System;
using System.IO;

using Workshell.FileFormats;
using tag_h.Persistence.Model;
using tag_h.Persistence;
using EphemeralEx.Injection;

namespace tag_h.Core.Model
{
    public class HFile : IDisposable
    {
        private HFileState _state;

        private readonly IDatabase _database;
        private readonly IFileHasher _hasher;

        public int Id => _state.Id;
        public string Location => _state.Location;
        
        public bool FileExists => File.Exists(Location);

        private readonly Lazy<Stream> _stream;
        public Stream Stream => _stream.Value;

        public FileFormat Format => _stream.IsValueCreated ? FileFormat.Get(Stream) : FileFormat.Get(Location);

        private readonly Lazy<FileHash> _hash;
        public FileHash Hash => _hash.Value;

        public HFile(IDatabase database, IFileHasher hasher, HFileState state)
        {
            _state = state;
            _database = database;
            _hasher = hasher;

            _hash = new Lazy<FileHash>(() => _hasher.GetHash(_state));
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
            _database.DeleteFile(_state);
        }
    }
}
