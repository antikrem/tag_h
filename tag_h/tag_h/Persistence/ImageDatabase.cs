using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (var command = _connection.CreateCommand())
            {
                command.CommandText
                    = @"INSERT INTO Images (fileName, tags, viewed, deleted) 
                        VALUES (@fileName, NULL, 0, 0);";
                command.Parameters.AddWithValue("@fileName", fileName);
                command.ExecuteNonQuery();
            }
        }

        public void SaveImage(HImage image)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText
                    = @"UPDATE Images
                        SET fileName = @fileName,
                            tags = @tags,
                            viewed = 1
                        WHERE id = @id;";
                command.Parameters.AddWithValue("@id", image.UUID);

                command.Parameters.AddWithValue("@fileName", image.Location);
                command.Parameters.AddWithValue("@tags", string.Join(", ", image.Tags));

                command.ExecuteNonQuery();
            }
        }

        public List<HImage> GetImageFromTag()
        {
            List<HImage> images = new List<HImage>();



            return images;
        }

        public List<HImage> FetchAllImages()
        {
            List<HImage> images = new List<HImage>();

            using (var command = _connection.CreateCommand())
            {
                command.CommandText
                    = @"SELECT * FROM Images;";

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    images.Add(
                            new HImage(dataReader.GetInt32(0), dataReader.GetString(1))
                        );
                }
            }

            return images;
        }

        public void DeleteImage(HImage image)
        {
            using (var command = _connection.CreateCommand())
            {
                command.CommandText
                    = @"UPDATE Images
                        SET delete = 1
                        WHERE id = @id;";

                command.Parameters.AddWithValue("@id", image.UUID);
                command.ExecuteNonQuery();
            }

            var location = image.Location;

            if (File.Exists(location))
            {
                File.Delete(location);
            }
        }
    }
}