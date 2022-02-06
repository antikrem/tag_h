using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EphemeralEx.Extensions;

using tag_h.Core.Model;
using tag_h.Core.Repositories;


namespace tag_h.Core.Tasks
{
    class SynchronisePersistence : ITask
    {
        private readonly IHFileRepository _imageRepository;

        public SynchronisePersistence(IHFileRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public string Name => "Synchronise Persistence";

        public Task Run()
        {
            _imageRepository.ApplyDeletions();

            var folder = _imageRepository.ImageFolder;

            var physicalImages = new HashSet<string>(folder.GetFiles().Select(x => x.FullName));
            var dbImages = _imageRepository.FetchFiles(FileQuery.All);
            var dbImageLocations = new HashSet<string>(dbImages.Select(x => x.Location));

            var newImages = physicalImages
                .Where(image => !dbImageLocations.Contains(image));

            var oldImages = dbImages
                .Where(image => !physicalImages.Contains(image.Location));

            foreach (var image in newImages)
            {
                _imageRepository.AddNewFile(image);
            }

            oldImages.ForEach(file => file.Delete());

            _imageRepository.ApplyDeletions();

            return Task.CompletedTask;
        }
    }
}