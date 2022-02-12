using System.Linq;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class AddNewFileQuery : IQuery<HFileState>
    {
        private string _location;

        public AddNewFileQuery(string location)
        {
            _location = location;
        }

        public HFileState Execute(ISQLCommandExecutor commandExecutor)
        {
            //TODO: Pretty bad, make these two happen together
            commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                    = @"INSERT INTO Files (fileName, deleted) 
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
                        FROM Files 
                        WHERE id = last_insert_rowid();";

                    return command
                        .ExecuteReader()
                        .GetHFiles()
                        .First();
                }
            );
        }
    }

}
