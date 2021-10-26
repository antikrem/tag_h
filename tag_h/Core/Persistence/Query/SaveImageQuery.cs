using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class SaveImageQuery : IQuery
    {
        HImage _image;

        public SaveImageQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText = CreateCommand(command);
                    command.Parameters.AddWithValue("@id", _image.Id);
                    command.Parameters.AddWithValue("@fileName", _image.Location);
                    command.ExecuteNonQuery();
                }
            );
        }

        private static string CreateCommand(SQLiteCommand command)
        {
            var body = @"UPDATE Images
                        SET fileName = @fileName,
                        WHERE id = @id;";

            return body;
        }
    }

}
