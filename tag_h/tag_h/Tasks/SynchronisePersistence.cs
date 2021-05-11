using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tag_h.Persistence;

namespace tag_h.Tasks
{
    class SynchronisePersistence : ITask
    {
        public string TaskName => "Synchronising Database";

        public void Execute(IHImageRepository imageRepository)
        {
            imageRepository.ApplyDeletions();

            var folder = imageRepository.ImageFolder;

            var physicalImages = new HashSet<FileInfo>(folder.GetFiles());
            var dbImages = imageRepository.FetchAllImages();
            var dbImageLocations = new HashSet<string>(dbImages.Select(x => x.Location));

            var newImages = physicalImages.ToList().Where(x => !dbImageLocations.Contains(x.FullName));

            foreach (var image in newImages)
            {
                imageRepository.AddNewImage(image.FullName);
            }
        }
    }
}
