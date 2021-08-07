using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class GetImageHashQuery : IQuery
    {
        private HImage _image;

        public ImageHash Hash;

        public GetImageHashQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"SELECT fileHash, perceptualHash
                        FROM Images
                        WHERE id = @id;";

            command.Parameters.AddWithValue("@id", _image.UUID);

            var reader = command.ExecuteReader();
            Hash = new ImageHash(reader.GetStringOrNull(0), reader.GetStringOrNull(1));
        }
    }

    class SetImageHashQuery : IQuery
    {
        private HImage _image;
        private readonly ImageHash _hash;

        public SetImageHashQuery(HImage image, ImageHash hash)
        {
            _image = image;
            _hash = hash;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"UPDATE Images
                        SET (fileHash, perceptualHash) = (@fileHash, @perceptualHash)
                        WHERE id = @id;";

            command.Parameters.AddWithValue("@id", _image.UUID);
            command.Parameters.AddWithValue("@fileHash", _hash.FileHash);
            command.Parameters.AddWithValue("@perceptualHash", _hash.PerceptualHash);
            command.ExecuteNonQuery();
        }
    }
}