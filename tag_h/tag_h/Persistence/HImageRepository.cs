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
<<<<<<< HEAD
        
        HImageList FetchAllDeletedImages();

        void SaveImage(HImage image);
=======
>>>>>>> ad0a6bc57e0cadeb477dd20cdda30b51ab3cf09b
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
<<<<<<< HEAD
            _imageDatabase.ClearDeletedImages(this);
=======
            _imageDatabase.ClearDeletedImages();
>>>>>>> ad0a6bc57e0cadeb477dd20cdda30b51ab3cf09b
        }

        public void AddNewImage(string fileName)
        {
            _imageDatabase.AddNewImage(fileName);
        }

<<<<<<< HEAD
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
=======

        public HImageList FetchAllImages()
        {
            return _imageDatabase.FetchAllImages();
>>>>>>> ad0a6bc57e0cadeb477dd20cdda30b51ab3cf09b
        }

        public HImageList FetchSampleHImages(int max)
        {
<<<<<<< HEAD
            return _imageDatabase.FetchSampleImageQueue(this, max);
        }

=======
            return _imageDatabase.FetchSampleImageQueue(max);
        }

        
>>>>>>> ad0a6bc57e0cadeb477dd20cdda30b51ab3cf09b

    }
}
