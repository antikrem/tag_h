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
        private readonly ITagRepository _tagRepository;

        public HFileFactory(IDatabase database, IFileHasher hasher, ITagRepository tagRepository)
        {
            _database = database;
            _hasher = hasher;
            _tagRepository = tagRepository;
        }

        public HFile Create(HFileState state)
            => new(_database, _hasher, _tagRepository, state);
    }
}
