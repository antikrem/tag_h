using System;
using System.IO;

using Workshell.FileFormats;

using tag_h.Persistence.Model;
using tag_h.Persistence;


namespace tag_h.Core.Model
{
    public class HFile : IDisposable
    {
        private HFileState _state;

        private readonly IDatabase _database;
        private readonly IFileHasher _hasher;
        private readonly ITagFactory _tagFactory;

        public int Id => _state.Id;
        public string Location => _state.Location;

        public bool FileExists => File.Exists(Location);

        private readonly Lazy<Stream> _stream;
        public Stream Stream => _stream.Value;

        public FileFormat Format => _stream.IsValueCreated ? FileFormat.Get(Stream) : FileFormat.Get(Location);

        private readonly Lazy<FileHash> _hash;
        public FileHash Hash => _hash.Value;

        public TagSet Tags => _tagFactory.Create(_database.GetTagsForFile(_state));

        public HFile(IDatabase database, IFileHasher hasher, ITagFactory tagFactory, HFileState state)
        {
            _state = state;

            _database = database;
            _hasher = hasher;
            _tagFactory = tagFactory;

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

        public void AddTag(Tag tag)
        {
            _database.AddTagToFile(_state, tag.State);
        }

        public void RemoveTag(Tag tag)
        {
            _database.RemoveTagFromFile(_state, tag.State);
        }
    }
}
