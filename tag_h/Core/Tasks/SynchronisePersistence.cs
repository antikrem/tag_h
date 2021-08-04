using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Model;
using tag_h.Core.Persistence;


namespace tag_h.Core.Tasks
{
    class SynchronisePersistence : ITask
    {
        public string TaskName => "Synchronising Database";

        public void Execute(IHImageRepository imageRepository, ITagRepository tagRepository, IImageHasher imageHasher)
        {
            imageRepository.ApplyDeletions();

            var folder = imageRepository.ImageFolder;

            var physicalImages = new HashSet<string>(folder.GetFiles().Select(x => x.FullName));
            var dbImages = imageRepository.FetchImages(TagQuery.All);
            var dbImageLocations = new HashSet<string>(dbImages.Select(x => x.Location));

            var newImages = physicalImages.ToList().Where(x => !dbImageLocations.Contains(x));

            var oldImages = dbImages.Where(x => !physicalImages.Contains(x.Location));

            foreach (var image in newImages)
            {
                imageRepository.AddNewImage(image);
            }

            foreach (var image in oldImages)
            {
                imageRepository.DeleteImage(image);
            }

            imageRepository.ApplyDeletions();

        }
    }
}
