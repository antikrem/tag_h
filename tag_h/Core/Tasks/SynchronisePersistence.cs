using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using tag_h.Core.Model;
using tag_h.Core.Persistence;


namespace tag_h.Core.Tasks
{
    class SynchronisePersistence : ITask
    {
        private readonly IHImageRepository _imageRepository;

        public SynchronisePersistence(IHImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public string Name => "Synchronise Persistence";

        public Task Run()
        {
            _imageRepository.ApplyDeletions();

            var folder = _imageRepository.ImageFolder;

            var physicalImages = new HashSet<string>(folder.GetFiles().Select(x => x.FullName));
            var dbImages = _imageRepository.FetchImages(ImageQuery.All);
            var dbImageLocations = new HashSet<string>(dbImages.Select(x => x.Location));

            var newImages = physicalImages
                .Where(image => !dbImageLocations.Contains(image));

            var oldImages = dbImages
                .Where(image => !physicalImages.Contains(image.Location));

            foreach (var image in newImages)
            {
                _imageRepository.AddNewImage(image);
            }

            foreach (var image in oldImages)
            {
                _imageRepository.DeleteImage(image);
            }

            _imageRepository.ApplyDeletions();

            return Task.CompletedTask;
        }
    }
}