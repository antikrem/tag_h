using System.Data.SQLite;

namespace tag_h.Persistence.Query
{
    class DeleteImageQuery : IQuery
    {
        private HImage _image;

        public DeleteImageQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"UPDATE Images
                        SET deleted = 1
                        WHERE id = @id;";

            command.Parameters.AddWithValue("@id", _image.UUID);
            command.ExecuteNonQuery();
        }
    }

}
