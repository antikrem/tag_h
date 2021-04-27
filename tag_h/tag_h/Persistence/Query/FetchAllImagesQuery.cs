using System.Collections.Generic;
using System.Data.SQLite;

namespace tag_h.Persistence.Query
{
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

}
