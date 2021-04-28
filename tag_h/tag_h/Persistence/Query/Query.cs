using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Persistence.Query
{

    interface IQuery
    {
        void Execute(SQLiteCommand command);
    }

    interface IWriteQuery
    {
        SQLiteDataReader Result { get; set; }
    }

    class SaveImageQuery : IQuery
    {
        HImage _image;

        public SaveImageQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"UPDATE Images
                        SET fileName = @fileName,
                            tags = @tags,
                            viewed = 1
                        WHERE id = @id;";
            command.Parameters.AddWithValue("@id", _image.UUID);

            command.Parameters.AddWithValue("@fileName", _image.Location);
            command.Parameters.AddWithValue("@tags", string.Join(", ", _image.Tags));

            command.ExecuteNonQuery();
        }
    }

    class FetchDeletedImagesQuery : IQuery
    {
        public List<HImage> Result { get; private set;  }

        public void Execute(SQLiteCommand command)
        {
            List<HImage> images = new List<HImage>();

            command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 1;";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                images.Add(
                        new HImage(dataReader.GetInt32(0), dataReader.GetString(1))
                    );
            }

            Result = images;
        }
    }

    class DeleteImageQuery : IQuery
    {
        private HImage _image;

        public DeleteImageQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"UPDATE Images
                        SET deleted = 1
                        WHERE id = @id;";

            command.Parameters.AddWithValue("@id", _image.UUID);
            command.ExecuteNonQuery();
        }
    }

    class AddNewImageQuery : IQuery
    {
        private string _location;

        public AddNewImageQuery(string location)
        {
            _location = location;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"INSERT INTO Images (fileName, tags, viewed, deleted) 
                        VALUES (@fileName, NULL, 0, 0);";
            command.Parameters.AddWithValue("@fileName", _location);
            command.ExecuteNonQuery();
        }
    }

    class FetchAllImagesQuery : IQuery
    {
        public List<HImage> Result { get; private set; }

        public void Execute(SQLiteCommand command)
        {
            List<HImage> images = new List<HImage>();

            command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 0;";

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                images.Add(
                        new HImage(dataReader.GetInt32(0), dataReader.GetString(1))
                    );
            }

            Result = images;
        }
    }

    class ClearDeletedImagesQuery : IQuery
    {
        private ImageDatabase _database;

        public ClearDeletedImagesQuery(ImageDatabase database)
        {
            _database = database;
        }

        public void Execute(SQLiteCommand command)
        {
            var images = _database.ExecuteQuery(new FetchDeletedImagesQuery()).Result;

            command.CommandText
                    = @"DELETE FROM Images
                        WHERE deleted = 1;";

            command.ExecuteNonQuery();

            foreach (var image in images)
            {
                if (File.Exists(image.Location))
                {
                    File.Delete(image.Location);
                }
            }

        }
    }

}
