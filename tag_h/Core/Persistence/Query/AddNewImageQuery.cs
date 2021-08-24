using System.Linq;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class AddNewImageQuery : IQuery
    {
        private string _location;

        public HImage Image { get; private set; }

        public AddNewImageQuery(string location)
        {
            _location = location;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT INTO Images (fileName, deleted) 
                        VALUES (@fileName, 0);";
                    command.Parameters.AddWithValue("@fileName", _location);

                    command.ExecuteNonQuery();
                },
                command =>
                {
                    command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE id = last_insert_rowid();";

                    Image = command
                        .ExecuteReader()
                        .GetHImages()
                        .First();
                }
            );
        }
    }

}
