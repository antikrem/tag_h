using System.IO;
using System.Linq;

using tagh.Core.Helper.Extensions;
using tagh.Core.Injection;
using tagh.Core.Model;

namespace tagh.Core.Persistence
{
    [Injectable]
    public interface IHImageRepository
    {
        //TODO
        DirectoryInfo ImageFolder { get; }

        void ApplyDeletions();

        void AddNewImage(string fileName);

        void SaveImage(HImage image);

        void DeleteImage(HImage image);

        HImageList FetchAllImages();

        HImageList FetchSampleHImages(int max);
    }

    public class HImageRepository : IHImageRepository
    {
        private IDatabase _database;

        public DirectoryInfo ImageFolder => _database.ImageFolder;

        public HImageRepository(IDatabase database)
        {
            _database = database;
        }

        public void ApplyDeletions()
        {
            var deletedImages = _database.GetDeletedImages();
            _database.RemoveDeletedImages();
            deletedImages
                .Select(x => x.Location)
                .Where(File.Exists)
                .ForEach(File.Delete);
        }

        public void AddNewImage(string fileName)
        {
            _database.AddNewImage(fileName);
        }


        public void SaveImage(HImage image)
        {
            _database.SaveImage(image);
        }

        public void DeleteImage(HImage image)
        {
            _database.DeleteImage(image);
        }

        public HImageList FetchAllImages()
        {
            return new HImageList(this, _database.FetchAllImages());
        }

        public HImageList FetchSampleHImages(int max)
        {
            return new HImageList(this, _database.FetchSampleImageQueue(max));
        }
    }
}
