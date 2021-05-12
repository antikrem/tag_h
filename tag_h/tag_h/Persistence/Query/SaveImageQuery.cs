using System.Data.SQLite;

using tag_h.Model;
using tag_h.Helper.Extensions;

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
            command.CommandText = CreateCommand(command);
            command.Parameters.AddWithValue("@id", _image.UUID);
            command.Parameters.AddWithValue("@fileName", _image.Location);
            command.ExecuteNonQuery();
        }

        private string CreateCommand(SQLiteCommand command)
        {
            var body = @"UPDATE Images
                        SET fileName = @fileName,
                            $hash$
                            viewed = 1
                        WHERE id = @id;";

            if (_image.Hash != null)
            {
                body = body.Replace("$hash$", "hash = @hash,");
                command.Parameters.AddWithValue("@hash", _image.Hash?.ToHexString());
            }
            else
            {
                body = body.Replace("$hash$", "");
            }

            return body;
        }
    }

}
