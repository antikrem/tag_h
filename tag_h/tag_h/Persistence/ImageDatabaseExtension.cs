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

        static public List<HImage> FetchAllImages(this IImageDatabase database)
        {
            return database.ExecuteQuery(new FetchAllImagesQuery()).Result;
        }

        static public List<HImage> FetchSampleImageQueue(this IImageDatabase database, int maxCount)
        {
            return database.ExecuteQuery(new FetchSampleImagesQuery(maxCount)).Result;
        }

        static public void DeleteImage(this IImageDatabase database, HImage image)
        {
            database.ExecuteQuery(new DeleteImageQuery(image));
        }

        static public TagSet GetTagsForImage(this IImageDatabase database, HImage image)
        {
            return database.ExecuteQuery(new FetchTagsForImageQuery(image)).Result;
        }

        static public void DeleteImageRecordFromDatabase(this IImageDatabase database, IEnumerable<HImage> images)
        {
            database.ExecuteQuery(new DeleteImageRecordFromDataBaseQuery(images));
        }

        static public List<HImage> GetDeletedImages(this IImageDatabase database)
        {
            return database.ExecuteQuery(new FetchDeletedImagesQuery()).Result;
        }
    }
}