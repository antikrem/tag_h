using System.Collections.Generic;
using System.Data.SQLite;
using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class FetchAllImagesQuery : IQuery
    {
        public HImageList Result { get; private set; }

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

            Result = new HImageList(images);
        }
    }

}
