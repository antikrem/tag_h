using System.Security.Cryptography;

using EphemeralEx.Extensions;
using EphemeralEx.Injection;

using tag_h.Core.Model;
using tag_h.Core.Persistence.Query;


namespace tag_h.Core.Persistence
{

    public record ImageHash(string FileHash, string PerceptualHash);

    [Injectable]
    public interface IImageHasher
    {
        ImageHash GetHash(HImage image);

        ImageHash HashImage(HImage image);
    }

    // TODO: stop on dejection, dispose MD5
    public class ImageHasher : IImageHasher
    {
        private readonly IDatabase _database;
        private readonly IPhysicalImageProvider _physicalImageProvider;

        private readonly MD5 _md5Hasher;

        public ImageHasher(IDatabase database, IPhysicalImageProvider physicalImageProvider)
        {
            _database = database;
            _physicalImageProvider = physicalImageProvider;
            
            _md5Hasher = MD5.Create();
        }

        public ImageHash GetHash(HImage image)
        {
            return _database.ExecuteQuery(new GetImageHashQuery(image)).Hash;
        }

        public ImageHash HashImage(HImage image)
        {
            using var stream = _physicalImageProvider.LoadImageStream(image); 
            var hash = _md5Hasher.ComputeHash(stream).ToHexString();

            _database.ExecuteQuery(
                    new SetImageHashQuery(image, new ImageHash(hash, null))
                );

            return new ImageHash(hash, null);
        }
    }

}