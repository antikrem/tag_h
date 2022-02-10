using tag_h.Core.Model;
using tag_h.Persistence;


namespace tag_h.Core.Persistence.Query
{
    public class AddNewTagQuery : IQuery<Tag>
    {
        private readonly string _name;

        public AddNewTagQuery(string name)
        {
            _name = name;
        }

        public Tag Execute(ISQLCommandExecutor commandExecutor)
        {
            var id = (int)commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT OR REPLACE INTO Tags
                        (name)
                        VALUES (@name)";

                    command.Parameters.AddWithValue("@name", _name);
                    command.ExecuteNonQuery();

                    return command.Connection.LastInsertRowId;
                }
            );
            return new Tag(id, _name);
        }
    }
}
