using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchDeletedImagesQuery : IQuery
    {

        public List<HImage> Result { get; private set; }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 1;";

                    Result = command
                        .ExecuteReader()
                        .GetHImages()
                        .ToList();
                }
            );
        }
    }

}
