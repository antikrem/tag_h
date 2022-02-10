using System.Collections.Generic;

using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchAllTagsQuery : IQuery<IEnumerable<TagState>>
    {
        public IEnumerable<TagState> Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT * FROM Tags;";

                    return command
                        .ExecuteReader()
                        .GetTags();
                }
            );
        }
    }
}
