﻿using System.Collections.Generic;
using System.Linq;

using tag_h.Model;
using tag_h.Persistence;

namespace tag_h.Tasks
{
    class DeleteDuplicates : ITask
    {
        public string TaskName => "Deleting Duplicate";

        public void Execute(IHImageRepository imageRepository)
        {
            var hashes = new Dictionary<ulong, string>();
            var duplicates = new List<(string, string)>();

            var dbImages = imageRepository.FetchAllImages()
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

            System.Console.WriteLine(duplicates);
        }
    }
}
