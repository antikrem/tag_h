using System.Data.SQLite;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class DeleteFileQuery : IQuery
    {
        private HFileState _file;

        public DeleteFileQuery(HFileState file)
        {
            _file = file;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"UPDATE Images
                        SET deleted = 1
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _file.Id);
                    command.ExecuteNonQuery();
                }
            );
        }
    }

}
