using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

using tag_h.Core.Model;

namespace tag_h.Core.Persistence.Query
{
    public class AddTagValue : IQuery
    {
        private readonly Tag _tag;
        private readonly string _value;

        public AddTagValue(Tag tag, string value)
        {
            _tag = tag;
            _value = value;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT OR REPLACE INTO TagValues
                        (id, value)
                        VALUES (@id, @value);";

                    command.Parameters.AddWithValue("@id", _tag.Id);
                    command.Parameters.AddWithValue("@value", _value);
                    command.ExecuteNonQuery();
                }
            );
        }
    }

    public class FetchTagValues : IQuery<List<string>>
    {
        private readonly Tag _tag;
        
        public FetchTagValues(Tag tag)
        {
            _tag = tag;
        }

        public List<string> Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT value 
                        FROM TagValues
                        WHERE id = @id;";

                    command.Parameters.AddWithValue("@id", _tag.Id);

                    return GetValues(command.ExecuteReader()).ToList();
                }
            );
        }

        private static IEnumerable<string> GetValues(SQLiteDataReader dataReader)
        {
            while (dataReader.Read())
                yield return dataReader.GetString(0);
        }
    }
}
