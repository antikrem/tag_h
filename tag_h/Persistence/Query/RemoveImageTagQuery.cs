using System.Data.SQLite;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class RemoveFileTagQuery : IQuery
    {
        private HFileState _file;
        private readonly TagState _tag;

        public RemoveFileTagQuery(HFileState file, TagState tag)
        {
            _file = file;
            _tag = tag;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"DELETE FROM FileTags
                        WHERE fileId = @fileId
                        AND tagId = @tagId;";

                    command.Parameters.AddWithValue("@fileId", _file.Id);
                    command.Parameters.AddWithValue("@tagId", _tag.Id);
                    command.ExecuteNonQuery();
                }
            );
        }
    }

}
