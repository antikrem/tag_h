using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class RemoveImageTagQuery : IQuery
    {
        private HImage _image;
        private readonly Tag _tag;

        public RemoveImageTagQuery(HImage image, Tag tag)
        {
            _image = image;
            _tag = tag;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"DELETE FROM ImageTags
                        WHERE imageId = @id
                        AND tag = @tag;";

            command.Parameters.AddWithValue("@id", _image.UUID);
            command.Parameters.AddWithValue("@tag", _tag.Name);
            command.ExecuteNonQuery();
        }
    }

}
