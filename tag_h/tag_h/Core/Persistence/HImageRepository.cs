using System.IO;
using System.Linq;

using tag_h.Core.Helper.Extensions;
using tag_h.Injection;
using tag_h.Core.Model;
using tag_h.Core.Persistence;

namespace tag_h.Core.Persistence
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

        HImageList FetchImages(TagQuery query);
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

        public HImageList FetchImages(TagQuery query)
        {
            return new HImageList(this, _database.FetchAllImages(query));
        }

    }
}
