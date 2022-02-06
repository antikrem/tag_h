using System.Data.SQLite;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class AddFileTagQuery : IQuery<bool>
    {
        private HFileState _file;
        private readonly Tag _tag;

        public AddFileTagQuery(HFileState file, Tag tag)
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
                    = @"INSERT OR IGNORE INTO ImageTags
                        (imageId, tagId)
                        VALUES (@imageId, @tagId);";

                    command.Parameters.AddWithValue("@imageId", _file.Id);
                    command.Parameters.AddWithValue("@tagId", _tag.Id);
                    return command.ExecuteNonQuery() == 1;
                }
            );
        }
    }
}
