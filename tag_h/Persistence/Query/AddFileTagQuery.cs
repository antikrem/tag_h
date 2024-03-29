using System.Data.SQLite;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class AddFileTagQuery : IQuery<bool>
    {
        private HFileState _file;
        private readonly TagState _tag;

        public AddFileTagQuery(HFileState file, TagState tag)
        {
            _file = file;
            _tag = tag;
        }

        public bool Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT OR IGNORE INTO FileTags
                        (fileId, tagId)
                        VALUES (@fileId, @tagId);";

                    command.Parameters.AddWithValue("@fileId", _file.Id);
                    command.Parameters.AddWithValue("@tagId", _tag.Id);
                    return command.ExecuteNonQuery() == 1;
                }
            );
        }
    }
}
