using System.Data.SQLite;
using tag_h.Persistence;

namespace tag_h.Core.Persistence.Query
{
    class RemoveDeletedImagesQuery : IQuery
    {
        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"DELETE FROM Images
                        WHERE deleted = 1;";

                    command.ExecuteNonQuery();
                }
            );
        }
    }

}
