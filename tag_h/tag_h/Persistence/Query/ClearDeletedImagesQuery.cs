using System.Data.SQLite;
using System.IO;

namespace tag_h.Persistence.Query
{
    class ClearDeletedImagesQuery : IQuery
    {
        private IHImageRepository _imageRepository;

        public ClearDeletedImagesQuery(IHImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public void Execute(SQLiteCommand command)
        {
            var images = _imageRepository.FetchAllDeletedImages();

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
