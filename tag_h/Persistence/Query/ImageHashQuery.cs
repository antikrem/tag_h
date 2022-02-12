using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.Persistence.Query
{
    class GetFileHashQuery : IQuery<FileHash?>
    {
        private HFileState _file;

        public GetFileHashQuery(HFileState file)
        {
            _file = file;
        }

        public FileHash? Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command => {
                    command.CommandText
                    = @"SELECT fileHash, perceptualHash
                        FROM Files
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _file.Id);

                    var reader = command.ExecuteReader();
                    // TODO: make extension
                    return reader.Read() 
                        ? new FileHash(reader.GetString(0), reader.GetStringOrNull(1)) 
                        : null;
                }
            );
        }
    }

    class SetFileHashQuery : IQuery
    {
        private HFileState _file;
        private readonly FileHash _hash;

        public SetFileHashQuery(HFileState file, FileHash hash)
        {
            _file = file;
            _hash = hash;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"UPDATE Files
                        SET (fileHash, perceptualHash) = (@fileHash, @perceptualHash)
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _file.Id);
                    command.Parameters.AddWithValue("@fileHash", _hash.DataHash);
                    command.Parameters.AddWithValue("@perceptualHash", _hash.PerceptualHash);
                    command.ExecuteNonQuery();
                }
            );
        }
    }
}
