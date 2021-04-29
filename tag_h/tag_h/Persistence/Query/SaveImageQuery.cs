using System.Data.SQLite;
using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class SaveImageQuery : IQuery
    {
        HImage _image;

        public SaveImageQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(SQLiteCommand command)
        {
            command.CommandText
                    = @"UPDATE Images
                        SET fileName = @fileName,
                            tags = @tags,
                            viewed = 1
                        WHERE id = @id;";
            command.Parameters.AddWithValue("@id", _image.UUID);

            command.Parameters.AddWithValue("@fileName", _image.Location);
            command.Parameters.AddWithValue("@tags", string.Join(", ", _image.Tags));

            command.ExecuteNonQuery();
        }
    }

}
