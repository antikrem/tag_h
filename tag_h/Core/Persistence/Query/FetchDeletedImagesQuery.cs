using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchDeletedImagesQuery : IQuery<List<HImage>>
    {
        public List<HImage> Execute(ISQLCommandExecutor commandExecutor)
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
                        .GetHImages()
                        .ToList();
                }
            );
        }
    }

}
