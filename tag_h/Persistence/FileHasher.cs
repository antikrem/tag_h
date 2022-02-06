using EphemeralEx.Injection;

using tag_h.Core.Persistence.Query;
using tag_h.Persistence.Model;


namespace tag_h.Persistence
{
    public record FileHash(string DataHash, string? PerceptualHash);

    [Injectable]
    public interface IFileHasher
    {
        FileHash GetHash(HFileState image);

        FileHash HashImage(HFileState image);
    }

    // TODO: stop on dejection, dispose MD5
    public class FileHasher : IFileHasher
    {
        private readonly IDatabase _database;
        private readonly IDataHasher _hasher;
        private readonly IPhysicalImageProvider _physicalImageProvider;


        public FileHasher(IDatabase database, IDataHasher hasher, IPhysicalImageProvider physicalImageProvider)
        {
            _database = database;
            _hasher = hasher;
            _physicalImageProvider = physicalImageProvider;
        }

        public FileHash GetHash(HFileState file) 
            => _database.ExecuteQuery(new GetImageHashQuery(file)) ?? HashImage(file);

        public FileHash HashImage(HFileState file)
        {
            using var stream = _physicalImageProvider.LoadFileStream(file);
            var hash = _hasher.Hash(stream);

            _database.ExecuteQuery(
                    new SetImageHashQuery(file, new FileHash(hash, null))
                );

            return new FileHash(hash, null);
        }
    }

}