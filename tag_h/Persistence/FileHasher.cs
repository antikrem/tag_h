using System.Security.Cryptography;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;

using tag_h.Core.Persistence.Query;
using tag_h.Persistence.Model;

namespace tag_h.Persistence
{

    public record FileHash(string Hash, string? PerceptualHash);

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
        private readonly IPhysicalImageProvider _physicalImageProvider;

        private readonly MD5 _md5Hasher;

        public FileHasher(IDatabase database, IPhysicalImageProvider physicalImageProvider)
        {
            _database = database;
            _physicalImageProvider = physicalImageProvider;

            _md5Hasher = MD5.Create();
        }

        public FileHash GetHash(HFileState file)
        {
            return _database.ExecuteQuery(new GetImageHashQuery(file)) ?? HashImage(file);
        }

        public FileHash HashImage(HFileState file)
        {
            using var stream = _physicalImageProvider.LoadFileStream(file);
            var hash = _md5Hasher.ComputeHash(stream).ToHexString();

            _database.ExecuteQuery(
                    new SetImageHashQuery(file, new FileHash(hash, null))
                );

            return new FileHash(hash, null);
        }
    }

}