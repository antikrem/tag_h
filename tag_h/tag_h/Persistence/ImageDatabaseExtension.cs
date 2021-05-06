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

        static public HImageList FetchAllImages(this IImageDatabase database)
        {
            return database.ExecuteQuery(new FetchAllImagesQuery()).Result;
        }

        static public HImageList FetchSampleImageQueue(this IImageDatabase database, int maxCount)
        {
            return database.ExecuteQuery(new FetchSampleImagesQuery(maxCount, database)).Result;
        }

        static public void DeleteImage(this IImageDatabase database, HImage image)
        {
            database.ExecuteQuery(new DeleteImageQuery(image));

        }

        static public TagSet GetTagsForImage(this IImageDatabase database, HImage image)
        {
            return database.ExecuteQuery(new FetchTagsForImageQuery(image)).Result;
        }

        static public void ClearDeletedImages(this IImageDatabase database)
        {
            database.ExecuteQuery(new ClearDeletedImagesQuery(database));
        }

        static private HImageList GetDeletedImages(this IImageDatabase database)
        {
            return database.ExecuteQuery(new FetchDeletedImagesQuery()).Result;
        }
    }
}