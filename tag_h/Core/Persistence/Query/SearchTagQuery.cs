using System.Linq;

using tag_h.Core.Model;

namespace tag_h.Core.Persistence.Query
{
    class SearchTagQuery : IQuery<Tag?>
    {
        string _value;

        public SearchTagQuery(string value)
        {
            _value = value;
        }

        public Tag? Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    // TODO: Optimise query to not use outer join
                    command.CommandText
                    = @"SELECT Tags.id, Tags.name 
                        FROM Tags LEFT OUTER JOIN TagValues 
                        ON Tags.id = TagValues.id
                        WHERE TagValues.value = @value OR Tags.name = @value;";

                    command.Parameters.AddWithValue("@value", _value);
                    
                    return command
                        .ExecuteReader()
                        .GetTags()
                        .FirstOrDefault();
                }
            );
        }
    }

}
