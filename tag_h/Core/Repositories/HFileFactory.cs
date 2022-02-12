using EphemeralEx.Injection;

using tag_h.Persistence.Model;
using tag_h.Persistence;


namespace tag_h.Core.Model
{
    [Injectable]
    public interface IHFileFactory
    {
        HFile Create(HFileState state);
    }

    public class HFileFactory : IHFileFactory
    {
        private readonly IDatabase _database;
        private readonly IFileHasher _hasher;
        private readonly ITagFactory _tagFactory;

        public HFileFactory(IDatabase database, IFileHasher hasher, ITagFactory tagFactory)
        {
            _database = database;
            _hasher = hasher;
            _tagFactory = tagFactory;
        }

        public HFile Create(HFileState state)
            => new(_database, _hasher, _tagFactory, state);
    }
}
