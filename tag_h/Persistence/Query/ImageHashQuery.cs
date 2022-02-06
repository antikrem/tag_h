using System.Data.SQLite;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class GetImageHashQuery : IQuery<FileHash?>
    {
        private HFileState _file;

        public GetImageHashQuery(HFileState file)
        {
            _file = file;
        }

        public FileHash? Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command => {
                    command.CommandText
                    = @"SELECT fileHash, perceptualHash
                        FROM Images
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _file.Id);

                    var reader = command.ExecuteReader();
                    // TODO: make extension
                    return reader.Read() 
                        ? new FileHash(reader.GetStringOrNull(0), reader.GetStringOrNull(1)) 
                        : null;
                }
            );
        }
    }

    class SetImageHashQuery : IQuery
    {
        private HFileState _file;
        private readonly FileHash _hash;

        public SetImageHashQuery(HFileState file, FileHash hash)
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
                    = @"UPDATE Images
                        SET (fileHash, perceptualHash) = (@fileHash, @perceptualHash)
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _file.Id);
                    command.Parameters.AddWithValue("@fileHash", _hash.Hash);
                    command.Parameters.AddWithValue("@perceptualHash", _hash.PerceptualHash);
                    command.ExecuteNonQuery();
                }
            );
        }
    }
}
