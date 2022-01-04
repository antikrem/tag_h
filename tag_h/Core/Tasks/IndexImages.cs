using System.Linq;
using System.Threading.Tasks;
using EphemeralEx.Extensions;

using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;


namespace tag_h.Core.Tasks
{
    class IndexImages : ITask
    {
        private readonly IHImageRepository _imageRepository;
        private readonly IImageHasher _imageHasher;

        public IndexImages(IHImageRepository imageRepository, IImageHasher imageHasher)
        {
            _imageRepository = imageRepository;
            _imageHasher = imageHasher;
        }

        public string Name => "Indexing images";

        public Task Run()
        {
            var unhashedImages = _imageRepository.FetchImages(ImageQuery.All)
                .Where(image => image.IsHashableFormat())
                .Where(image => _imageHasher.GetHash(image).FileHash == null);

            unhashedImages.ForEach(image => _imageHasher.HashImage(image));

            return Task.CompletedTask;
        }
    }
}
