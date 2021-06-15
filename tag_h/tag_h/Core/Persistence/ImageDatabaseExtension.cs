using System.Collections.Generic;

using tag_h.Core.Model;
using tag_h.Core.Persistence.Query;


namespace tag_h.Core.Persistence
{
    static class ImageDatabaseExtension
    {

        static public void AddNewImage(this IDatabase database, string fileName)
        {
            database.ExecuteQuery(new AddNewImageQuery(fileName));
        }

        static public void SaveImage(this IDatabase database, HImage image)
        {
            database.ExecuteQuery(new SaveImageQuery(image));
        }

        static public List<HImage> FetchAllImages(this IDatabase database, TagQuery query)
        {
            return database.ExecuteQuery(new FetchImagesQuery(query)).Result;
        }

        static public List<HImage> FetchSampleImageQueue(this IDatabase database, int maxCount)
        {
            return database.ExecuteQuery(new FetchSampleImagesQuery(maxCount)).Result;
        }

        static public void DeleteImage(this IDatabase database, HImage image)
        {
            database.ExecuteQuery(new DeleteImageQuery(image));
        }

        static public TagSet GetTagsForImage(this IDatabase database, HImage image)
        {
            return database.ExecuteQuery(new FetchTagsForImageQuery(image)).Result;
        }

        static public void RemoveDeletedImages(this IDatabase database)
        {
            database.ExecuteQuery(new RemoveDeletedImagesQuery());
        }

        static public List<HImage> GetDeletedImages(this IDatabase database)
        {
            return database.ExecuteQuery(new FetchDeletedImagesQuery()).Result;
        }
    }
}