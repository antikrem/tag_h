using System.IO;

using tag_h.Model;

namespace tag_h.Persistence
{
    public interface IHImageRepository
    {
        //TODO
        DirectoryInfo ImageFolder { get; }

        void ApplyDeletions();

        void AddNewImage(string fileName);
        
        HImageList FetchAllImages();
        
        HImageList FetchSampleHImages(int max);
        
        HImageList FetchAllDeletedImages();

        void SaveImage(HImage image);
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
            _imageDatabase.ClearDeletedImages(this);
        }

        public void AddNewImage(string fileName)
        {
            _imageDatabase.AddNewImage(fileName);
        }

        public void SaveImage(HImage image)
        {
            _imageDatabase.SaveImage(image);
        }

        public HImageList FetchAllImages()
        {
            return _imageDatabase.FetchAllImages(this);
        }

        public HImageList FetchAllDeletedImages()
        {
            return _imageDatabase.GetDeletedImages(this);
        }

        public HImageList FetchSampleHImages(int max)
        {
            return _imageDatabase.FetchSampleImageQueue(this, max);
        }


    }
}
