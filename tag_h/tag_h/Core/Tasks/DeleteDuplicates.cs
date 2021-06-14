using System.Collections.Generic;
using System.Linq;
using tagh.Core.Model;
using tagh.Core.Persistence;

namespace tagh.Core.Tasks
{
    class DeleteDuplicates : ITask
    {
        public string TaskName => "Deleting Duplicate";

        public void Execute(IHImageRepository imageRepository)
        {
            var hashes = new Dictionary<ulong, string>();
            var duplicates = new List<(string, string)>();

            using (var images = imageRepository.FetchImages(TagQuery.All))
            {
                var dbImages = images
                    .Where(x => x.IsHashableFormat())
                    .Select(image => (image, image.Hash))
                    .Where(y => y.Hash != null)
                    .Select(y => (y.image, y.Hash.Value));

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
}
