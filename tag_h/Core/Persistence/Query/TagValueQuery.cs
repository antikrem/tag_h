using System.Collections.Generic;
using System.Data.SQLite;

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

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"INSERT OR REPLACE INTO TagValues
                        (id, value)
                        VALUES (@id, @value);";

            command.Parameters.AddWithValue("@id", _tag.Id);
            command.Parameters.AddWithValue("@value", _value);
            command.ExecuteNonQuery();
        }
    }
}
