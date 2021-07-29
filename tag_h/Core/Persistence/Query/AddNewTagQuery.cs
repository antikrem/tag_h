using System.Data.SQLite;
using tag_h.Core.Model;

namespace tag_h.Core.Persistence.Query
{
    public class AddNewTagQuery : IQuery
    {
        private readonly Tag _tag;

        public AddNewTagQuery(Tag tag)
        {
            _tag = tag;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"INSERT OR REPLACE INTO Tags
                        (tag)
                        VALUES (@tag);";

            command.Parameters.AddWithValue("@tag", _tag.Value);
            command.ExecuteNonQuery();
        }
    }
}
