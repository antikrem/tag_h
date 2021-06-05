using System.IO;
using System.Linq;
using tag_h.Helper.Extensions;
using tag_h.Model;

namespace tag_h.Persistence
{
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
        private IImageDatabase _imageDatabase;

        public DirectoryInfo ImageFolder => _imageDatabase.ImageFolder;

        public HImageRepository(IImageDatabase imageDatabase)
        {
            _imageDatabase = imageDatabase;
        }

        public void ApplyDeletions()
        {
            var deletedImages = _imageDatabase.GetDeletedImages();
            _imageDatabase.DeleteImageRecordFromDatabase();
            deletedImages
                .Select(x => x.Location)
                .Where(File.Exists)
                .ForEach(File.Delete);
        }

        public void AddNewImage(string fileName)
        {
            _imageDatabase.AddNewImage(fileName);
        }


        public void SaveImage(HImage image)
        {
            _imageDatabase.SaveImage(image);
        }

        public void DeleteImage(HImage image)
        {
            _imageDatabase.DeleteImage(image);
        }

        public HImageList FetchAllImages()
        {
            return new HImageList(this, _imageDatabase.FetchAllImages());
        }

        public HImageList FetchSampleHImages(int max)
        {
            return new HImageList(this, _imageDatabase.FetchSampleImageQueue(max));
        }
    }
}
