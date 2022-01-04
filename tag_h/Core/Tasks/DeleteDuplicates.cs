using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tag_h.Core.Model;
using tag_h.Core.Persistence;
using tag_h.Core.TagRetriever;

namespace tag_h.Core.Tasks
{
    class DeleteDuplicates : ITask
    {
        private readonly IImageHasher _imageHasher;
        private readonly IHImageRepository _imageRepository;

        public DeleteDuplicates(IImageHasher imageHasher)
        {
            _imageHasher = imageHasher;
        }

        public string Name => "Delete Duplicates";

        public Task Run()
        {
            var hashes = new Dictionary<string, string>();
            var duplicates = new List<(string, string)>();

            var dbImages = _imageRepository.FetchImages(ImageQuery.All)
                .Where(image => image.IsHashableFormat())
                .Select(image => (image, Hash: _imageHasher.GetHash(image).PerceptualHash))
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

            System.Console.WriteLine($"Found {duplicates.Count} duplicate image/s"); //TODO: replace with logger
            return Task.CompletedTask;
        }

    }
}
