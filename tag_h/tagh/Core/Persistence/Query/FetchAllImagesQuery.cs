using System.Collections.Generic;
using System.Data.SQLite;

using tagh.Core.Model;

namespace tagh.Core.Persistence.Query
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
                images.Add(dataReader.GetHImage());
            }

            Result = images;
        }

    }

}
