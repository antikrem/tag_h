using System.Collections.Generic;
using tag_h.Model;
using tag_h.Persistence.Query;

namespace tag_h.Persistence
{
    static class ImageDatabaseExtension
    {

        static public void AddNewImage(this IImageDatabase database, string fileName)
        {
            database.ExecuteQuery(new AddNewImageQuery(fileName));
        }

        static public void SaveImage(this IImageDatabase database, HImage image)
        {
            database.ExecuteQuery(new SaveImageQuery(image));
        }

        static public HImageList FetchAllImages(this IImageDatabase database, IHImageRepository imageRepository)
        {
            return database.ExecuteQuery(new FetchAllImagesQuery(imageRepository)).Result;
        }

        static public HImageList FetchSampleImageQueue(this IImageDatabase database, IHImageRepository imageRepository, int maxCount)
        {
            return database.ExecuteQuery(new FetchSampleImagesQuery(maxCount, imageRepository)).Result;
        }

        static public void DeleteImage(this IImageDatabase database, HImage image)
        {
            database.ExecuteQuery(new DeleteImageQuery(image));
        }

        static public TagSet GetTagsForImage(this IImageDatabase database, HImage image)
        {
            return database.ExecuteQuery(new FetchTagsForImageQuery(image)).Result;
        }

        static public void ClearDeletedImages(this IImageDatabase database, IHImageRepository imageRepository)
        {
            database.ExecuteQuery(new ClearDeletedImagesQuery(imageRepository));
        }

        static public HImageList GetDeletedImages(this IImageDatabase database, IHImageRepository imageRepository)
        {
            return database.ExecuteQuery(new FetchDeletedImagesQuery(imageRepository)).Result;
        }
    }
}