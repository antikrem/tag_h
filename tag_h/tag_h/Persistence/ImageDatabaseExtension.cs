﻿using System.Collections.Generic;
using tag_h.Persistence.Query;

namespace tag_h.Persistence
{
    static class ImageDatabaseExtension
    {

        static public void AddNewImage(this ImageDatabase database, string fileName)
        {
            database.ExecuteQuery(new AddNewImageQuery(fileName));
        }

        static public void SaveImage(this ImageDatabase database, HImage image)
        {
            database.ExecuteQuery(new SaveImageQuery(image));
        }

        static public List<HImage> FetchAllImages(this ImageDatabase database)
        {
            return database.ExecuteQuery(new FetchAllImagesQuery()).Result;
        }

        static public void DeleteImage(this ImageDatabase database, HImage image)
        {
            database.ExecuteQuery(new DeleteImageQuery(image));

        }

        static public void ClearDeletedImages(this ImageDatabase database)
        {
            database.ExecuteQuery(new ClearDeletedImagesQuery(database));
        }

        static private List<HImage> GetDeletedImages(this ImageDatabase database)
        {
            return database.ExecuteQuery(new FetchDeletedImagesQuery()).Result;
        }
    }
}