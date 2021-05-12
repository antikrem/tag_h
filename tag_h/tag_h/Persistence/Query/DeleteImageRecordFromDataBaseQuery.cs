using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class DeleteImageRecordFromDataBaseQuery : IQuery
    {
        private IHImageRepository _imageRepository;

        private IEnumerable<HImage> _images;

        public DeleteImageRecordFromDataBaseQuery(IEnumerable<HImage> images)
        {
            _images = images;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"DELETE FROM Images
                        WHERE deleted = 1;";

            command.ExecuteNonQuery();
        }
    }

}
