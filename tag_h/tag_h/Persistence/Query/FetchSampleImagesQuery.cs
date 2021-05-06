using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class FetchSampleImagesQuery : IQuery
    {
        public HImageList Result { get; private set; }

        private int _maxCount;
        private readonly IImageDatabase _imageDatabase;

        public FetchSampleImagesQuery(int maxCount, IImageDatabase imageDatabase)
        {
            _maxCount = maxCount;
            _imageDatabase = imageDatabase;
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
                var image = dataReader.GetHImage();
                if (image.IsPhysicalExists())
                {
                    images.Add(image);
                }
            }

            foreach (var image in images)
            {
                image.Tags = _imageDatabase.GetTagsForImage(image);
            }

            Result = new HImageList(images);
        }
    }
}
