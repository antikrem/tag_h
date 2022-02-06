using System.Linq;
using System.Threading.Tasks;
using EphemeralEx.Extensions;

using tag_h.Core.Model;
using tag_h.Core.Repositories;
using tag_h.Persistence;

namespace tag_h.Core.Tasks
{
    class IndexImages : ITask
    {
        private readonly IHFileRepository _fileRepository;
        private readonly IFileHasher _imageHasher;

        public IndexImages(IHFileRepository fileRepository, IFileHasher imageHasher)
        {
            _fileRepository = fileRepository;
            _imageHasher = imageHasher;
        }

        public string Name => "Indexing images";

        public Task Run()
        {
            var unhashedImages = _fileRepository.FetchFiles(FileQuery.All)
                .Where(image => image.IsHashableFormat());
                //.Where(image => _imageHasher.GetHash(image).FileHash == null); TODO

            //unhashedImages.ForEach(image => _imageHasher.HashImage(image));

            return Task.CompletedTask;
        }
    }
}
