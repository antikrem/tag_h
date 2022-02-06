using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using tag_h.Core.Model;
using tag_h.Core.Repositories;
using tag_h.Persistence;


namespace tag_h.Core.Tasks
{
    class DeleteDuplicates : ITask
    {
        private readonly IFileHasher _fileHasher;
        private readonly IHFileRepository _fileRepository;

        public DeleteDuplicates(IFileHasher imageHasher, IHFileRepository fileRepository)
        {
            _fileHasher = imageHasher;
            _fileRepository = fileRepository;
        }

        public string Name => "Delete Duplicates";

        public Task Run()
        {
            var hashes = new Dictionary<string, string>();
            var duplicates = new List<(string, string)>();

            var dbFiles = _fileRepository.FetchFiles(FileQuery.All)
                .Where(file => file.IsHashableFormat())
                .Where(file => file.Hash.DataHash != null);

            foreach (var file in dbFiles)
            {
                var hash = file.Hash.DataHash;
                if (hashes.ContainsKey(hash))
                    duplicates.Add((hashes[hash], file.Location));
                else
                    hashes[hash] = file.Location;
            }

            System.Console.WriteLine($"Found {duplicates.Count} duplicate image/s"); //TODO: replace with logger
            return Task.CompletedTask;
        }

    }
}
