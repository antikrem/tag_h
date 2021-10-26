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
            using (var images = imageRepository.FetchImages(ImageQuery.All))
            {
                var unhashedImages = images
                    .Where(x => x.IsHashableFormat())
                    .Where(y => imageHasher.GetHash(y).FileHash == null);

                unhashedImages.ForEach(
                        image => imageHasher.HashImage(image)
                    );
            }
        }
    }
}
