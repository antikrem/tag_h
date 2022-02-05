using System.Linq;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class AddNewImageQuery : IQuery<HImage>
    {
        private string _location;

        public AddNewImageQuery(string location)
        {
            _location = location;
        }

        public HImage Execute(ISQLCommandExecutor commandExecutor)
        {
            //TODO: Pretty bad, make these two happen together
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT INTO Images (fileName, deleted) 
                        VALUES (@fileName, 0);";
                    command.Parameters.AddWithValue("@fileName", _location);

                    command.ExecuteNonQuery();
                }                
            );

            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE id = last_insert_rowid();";

                    return command
                        .ExecuteReader()
                        .GetHImages()
                        .First();
                }
            );
        }
    }

}
