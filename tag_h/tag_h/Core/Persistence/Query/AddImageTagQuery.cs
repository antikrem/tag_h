using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class AddImageTagQuery : IQuery
    {
        private HImage _image;
        private readonly Tag _tag;

        public AddImageTagQuery(HImage image, Tag tag)
        {
            _image = image;
            _tag = tag;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"INSERT INTO ImageTags
                        (id, tag)
                        VALUES (@id, @tag);";

            command.Parameters.AddWithValue("@id", _image.UUID);
            command.Parameters.AddWithValue("@tag", _tag.Value);
            command.ExecuteNonQuery();
        }
    }

}
