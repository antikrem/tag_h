using System.Collections.Generic;
using System.Linq;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class FetchDeletedFilesQuery : IQuery<List<HFileState>>
    {
        public List<HFileState> Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 1;";

                    return command
                        .ExecuteReader()
                        .GetHFiles()
                        .ToList();
                }
            );
        }
    }

}
