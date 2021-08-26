using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class AddImageTagQuery : IQuery
    {
        private HImage _image;
        private readonly Tag _tag;

        public bool Success { get; private set; }

        public AddImageTagQuery(HImage image, Tag tag)
        {
            _image = image;
            _tag = tag;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT OR IGNORE INTO ImageTags
                        (imageId, tagId)
                        VALUES (@imageId, @tagId);";

                    command.Parameters.AddWithValue("@imageId", _image.Id);
                    command.Parameters.AddWithValue("@tagId", _tag.Id);
                    Success = command.ExecuteNonQuery() == 1;
                }
            );
        }
    }

}
