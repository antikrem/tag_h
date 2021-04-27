using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Tasks
{
    class SynchronisePersistence : ITask
    {
        public string TaskName => "Synchronising Database";

        public void Execute(Persistence.ImageDatabase database)
        {
            var folder = database.ImageFolder;

            var physicalImages = new HashSet<FileInfo>(folder.GetFiles());
            var dbImages = database.FetchAllImages();
            var dbImageLocations = new HashSet<string>(dbImages.Select(x => x.Location));

            var newImages = physicalImages.ToList().Where(x => !dbImageLocations.Contains(x.FullName));

            foreach (var image in newImages)
            {
                database.AddNewImage(image.FullName);
            }
        }
    }
}
