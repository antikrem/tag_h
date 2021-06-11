using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagh.Core.Persistence;

namespace tagh.Core.Tasks
{
    class SynchronisePersistence : ITask
    {
        public string TaskName => "Synchronising Database";

        public void Execute(IHImageRepository imageRepository)
        {
            imageRepository.ApplyDeletions();

            var folder = imageRepository.ImageFolder;

            var physicalImages = new HashSet<string>(folder.GetFiles().Select(x => x.FullName));
            var dbImages = imageRepository.FetchAllImages();
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
