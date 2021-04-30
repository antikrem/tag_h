using System.Data.SQLite;
using System.IO;

namespace tag_h.Persistence.Query
{
    class ClearDeletedImagesQuery : IQuery
    {
        private IImageDatabase _database;

        public ClearDeletedImagesQuery(IImageDatabase database)
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
