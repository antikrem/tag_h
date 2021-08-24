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

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT fileHash, perceptualHash
                        FROM Images
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _image.Id);

                    var reader = command.ExecuteReader();
                    // TODO: make extension
                    if (reader.Read())
                        Hash = new ImageHash(reader.GetStringOrNull(0), reader.GetStringOrNull(1));
                }
            );
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

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"UPDATE Images
                        SET (fileHash, perceptualHash) = (@fileHash, @perceptualHash)
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _image.Id);
                    command.Parameters.AddWithValue("@fileHash", _hash.FileHash);
                    command.Parameters.AddWithValue("@perceptualHash", _hash.PerceptualHash);
                    command.ExecuteNonQuery();
                }
            );
        }
    }
}
