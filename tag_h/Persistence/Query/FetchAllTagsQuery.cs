using System.Data.SQLite;

using tag_h.Core.Model;
using tag_h.Persistence;

namespace tag_h.Core.Persistence.Query
{
    class FetchAllTagsQuery : IQuery<TagSet>
    {
        public TagSet Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT * FROM Tags;";

                    return command.ExecuteReader().GetTags();
                }
            );
        }
    }
}
