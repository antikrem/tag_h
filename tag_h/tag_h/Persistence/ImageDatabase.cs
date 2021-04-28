using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tag_h.Persistence.Query;

namespace tag_h.Persistence
{
    class ImageDatabase : IDisposable
    {

        private DatabaseConnection _connection;

        public DirectoryInfo ImageFolder {
            get
            {
                if (!Directory.Exists("Images/"))
                {
                    Directory.CreateDirectory("Images/");
                }
                return new DirectoryInfo("Images/");
            }
        }
        

        public ImageDatabase()
        {
            _connection = new DatabaseConnection();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public void AddNewImage(string fileName)
        {
            ExecuteQuery(new AddNewImageQuery(fileName));
        }

        public void SaveImage(HImage image)
        {
            ExecuteQuery(new SaveImageQuery(image));
        }

        public List<HImage> GetImageFromTag()
        {
            List<HImage> images = new List<HImage>();



            return images;
        }

        public List<HImage> FetchAllImages()
        {
            return ExecuteQuery(new FetchAllImagesQuery()).Result;
        }

        public void DeleteImage(HImage image)
        {
            ExecuteQuery(new DeleteImageQuery(image));

        }

        public void ClearDeletedImages()
        {
            ExecuteQuery(new ClearDeletedImagesQuery(this));
        }

        private List<HImage> GetDeletedImages()
        {
            return ExecuteQuery(new FetchDeletedImagesQuery()).Result;
        }


        public T ExecuteQuery<T>(T query) where T : IQuery
        {
            using (var command = _connection.CreateCommand())
            {
                query.Execute(command);
            }

            return query;
        }
    }
}