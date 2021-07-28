using System.IO;
using System.Linq;

using tag_h.Core.Helper.Extensions;
using tag_h.Injection;
using tag_h.Core.Model;

namespace tag_h.Core.Persistence
{
    [Injectable]
    public interface IHImageRepository
    {
        //TODO
        DirectoryInfo ImageFolder { get; }

        void ApplyDeletions();

        void AddNewImage(string fileName);

        HImage CreateNewImage(string fileName, byte[] data);

        void SaveImage(HImage image);

        void DeleteImage(HImage image);

        HImageList FetchImages(TagQuery query);
    }

    public class HImageRepository : IHImageRepository
    {
        private IDatabase _database;
        private readonly IPhysicalImageProvider _physicalImageProvider;

        public DirectoryInfo ImageFolder => _database.ImageFolder;

        public HImageRepository(IDatabase database, IPhysicalImageProvider physicalImageProvider)
        {
            _database = database;
            _physicalImageProvider = physicalImageProvider;
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

        public HImage CreateNewImage(string fileName, byte[] data)
        {
            _physicalImageProvider.CreatePhysicalImage(_database.ImageFolder.FullName, fileName, data);
            fileName = Path.Join(_database.ImageFolder.FullName, fileName);
            _database.AddNewImage(fileName);
            return FetchImages(new TagQuery { Location = fileName }).First();
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
