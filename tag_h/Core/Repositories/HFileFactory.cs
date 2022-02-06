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

        public HFileFactory(IDatabase database, IFileHasher hasher)
        {
            _database = database;
            _hasher = hasher;
        }

        public HFile Create(HFileState state)
            => new(_database, _hasher , state);
    }
}
