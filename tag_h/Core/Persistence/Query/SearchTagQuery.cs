using System.Data.SQLite;

using tag_h.Core.Model;

namespace tag_h.Core.Persistence.Query
{
    class SearchTagQuery : IQuery
    {
        string _value;

        public Tag Result { get; private set; }

        public SearchTagQuery(string value)
        {
            _value = value;
        }

        public void Execute(SQLiteCommand command)
        {
            // TODO: Optimise query to not use outer join
            command.CommandText
                    = @"SELECT Tags.id, Tags.name 
                        FROM Tags LEFT OUTER JOIN TagValues 
                        ON Tags.id = TagValues.id
                        WHERE TagValues.value = @value OR Tags.name = @value;";

            command.Parameters.AddWithValue("@value", _value);

            //TODO: move to extension
            var reader = command.ExecuteReader();
            if (reader.Read())
                Result = reader.GetTag();
        }
    }

}
