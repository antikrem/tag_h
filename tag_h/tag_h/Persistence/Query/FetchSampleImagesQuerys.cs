using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class FetchSampleImagesQuerys : IQuery
    {
        public HImageList Result { get; private set; }

        private int _maxCount;

        public FetchSampleImagesQuerys(int maxCount)
        {
            _maxCount = maxCount;
        }

        public void Execute(SQLiteCommand command)
        {
            List<HImage> images = new List<HImage>();

            command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 0;";

            var dataReader = command.ExecuteReader();

            int runningCount = 0;
            while (dataReader.Read() && runningCount < _maxCount)
            {
                var image = new HImage(dataReader.GetInt32(0), dataReader.GetString(1));
                if (image.IsPhysicalExists())
                {
                    images.Add(image);
                }
            }

            Result = new HImageList(images);
        }
    }
}
