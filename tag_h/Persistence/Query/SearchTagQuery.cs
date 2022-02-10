using System.Linq;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.Persistence.Query
{
    class SearchTagQuery : IQuery<TagState?>
    {
        string _value;

        public SearchTagQuery(string value)
        {
            _value = value;
        }

        public TagState? Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
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
