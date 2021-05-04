using System.Collections.Generic;
using System.Linq;

using tag_h.Model;
using tag_h.Persistence;

namespace tag_h.Tasks
{
    class DeleteDuplicates : ITask
    {
        public string TaskName => "Deleting Duplicate";

        public void Execute(IImageDatabase database)
        {
            var hashes = new Dictionary<ulong, string>();
            var duplicates = new List<(string, string)>();

            var dbImages 
                = database.FetchAllImages()
                .Where(x => x.IsHashableFormat()) ;

            foreach (var image in dbImages)
            {
                var hash = image.Hash;

                if (hash is null)
                {
                    continue;
                }

                if (hashes.ContainsKey(hash.Value))
                {
                    duplicates.Add((hashes[hash.Value], image.Location));
                }
                else
                {
                    hashes[hash.Value] = image.Location;
                }
            }

            System.Console.WriteLine(duplicates);
        }
    }
}
