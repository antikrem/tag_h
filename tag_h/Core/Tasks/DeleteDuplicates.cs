using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;

namespace tag_h.Core.Tasks
{
    class DeleteDuplicates : ITask
    {
        public string TaskName => "Deleting Duplicate";

        public void Execute(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher, IAutoTagger autoTagger)
        {
            var hashes = new Dictionary<string, string>();
            var duplicates = new List<(string, string)>();

            var dbImages = imageRepository.FetchImages(ImageQuery.All)
                .Where(image => image.IsHashableFormat())
                .Select(image => (image, Hash: imageHasher.GetHash(image).PerceptualHash))
                .Where(image => image.Hash != null);

            foreach (var (image, hash) in dbImages)
            {
                if (hashes.ContainsKey(hash))
                {
                    duplicates.Add((hashes[hash], image.Location));
                }
                else
                {
                    hashes[hash] = image.Location;
                }
            }

            System.Console.WriteLine($"Found {duplicates.Count} duplicate image/s");
        }
    }
}
