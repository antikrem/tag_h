using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchImagesQuery : IQuery
    {
        private readonly ImageQuery _query;

        public List<HImage> Result { get; private set; }

        public FetchImagesQuery(ImageQuery query)
        {
            _query = query;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            List<HImage> images = new List<HImage>();

            commandExecutor.ExecuteCommand(
                command =>
                {
                    var commandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE $WHERECLAUSE
                        $LIMIT;";
                    commandText = commandText.Replace("$LIMIT", _query.Maximum != int.MaxValue ? $"LIMIT {_query.Maximum}" : "");
                    commandText = commandText.Replace("$WHERECLAUSE", string.Join(" AND ", BuildWhereClause()));

                    command.CommandText = commandText;

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        images.Add(dataReader.GetHImage());
                    }
                }
            );

            Result = images;

        }
        private IEnumerable<string> BuildWhereClause()
        {
            yield return " deleted = 0";

            if (_query.Id > 0)
                yield return $"id = {_query.Id}";

            if (_query.Location != null)
                yield return $"fileName = '{_query.Location}'";
        }
    }

}
