using System.Linq;

using EphemeralEx.Extensions;

using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;


namespace tag_h.Core.Tasks
{
    class IndexImages : ITask
    {
        public string TaskName => "Indexing all Images";

        public void Execute(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher, IAutoTagger autoTagger)
        {
            var unhashedImages = imageRepository.FetchImages(ImageQuery.All)
                .Where(image => image.IsHashableFormat())
                .Where(image => imageHasher.GetHash(image).FileHash == null);

            unhashedImages.ForEach(image => imageHasher.HashImage(image));
        }
    }
}
