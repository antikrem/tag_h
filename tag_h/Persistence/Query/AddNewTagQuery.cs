using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.Persistence.Query
{
    public class AddNewTagQuery : IQuery<TagState>
    {
        private readonly string _name;

        public AddNewTagQuery(string name)
        {
            _name = name;
        }

        public TagState Execute(ISQLCommandExecutor commandExecutor)
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
            return new(id, _name);
        }
    }
}
