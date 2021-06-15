using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchImagesQuery : IQuery
    {
        private readonly TagQuery _query;

        public List<HImage> Result { get; private set; }

        public FetchImagesQuery(TagQuery query)
        {
            _query = query;
        }

        public void Execute(SQLiteCommand command)
        {
            List<HImage> images = new List<HImage>();

            var commandText
                    = @"SELECT * 
                        FROM Images 
                        WHERE deleted = 0
                        $LIMIT;";
            commandText = commandText.Replace("$LIMIT", _query.Maximum != int.MaxValue ? $"LIMIT {_query.Maximum}" : "");

            command.CommandText = commandText;

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                images.Add(dataReader.GetHImage());
            }

            Result = images;
        }

    }

}
