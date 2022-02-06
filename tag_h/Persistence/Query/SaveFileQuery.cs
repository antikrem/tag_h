using System.Data.SQLite;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class SaveFileQuery : IQuery
    {
        HFileState _file;

        public SaveFileQuery(HFileState file)
        {
            _file = file;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText = CreateCommand();
                    command.Parameters.AddWithValue("@id", _file.Id);
                    command.Parameters.AddWithValue("@fileName", _file.Location);
                    command.ExecuteNonQuery();
                }
            );
        }

        private static string CreateCommand() 
            => @"UPDATE Images
                SET fileName = @fileName,
                WHERE id = @id;";
    }

}
