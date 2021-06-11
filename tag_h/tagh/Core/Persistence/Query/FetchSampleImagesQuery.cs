using System.Collections.Generic;
using System.Data.SQLite;
using tagh.Core.Model;

namespace tagh.Core.Persistence.Query
{
    class FetchSampleImagesQuery : IQuery
    {
        public List<HImage> Result { get; private set; }

        private int _maxCount;

        public FetchSampleImagesQuery(int maxCount)
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

            while (dataReader.Read() && images.Count < _maxCount)
            {
                var image = dataReader.GetHImage();
                if (image.IsPhysicalExists())
                {
                    images.Add(image);
                }
            }

            Result = images;
        }
    }
}
