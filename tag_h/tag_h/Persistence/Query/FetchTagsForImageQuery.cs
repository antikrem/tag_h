using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Model;

namespace tag_h.Persistence.Query
{
    class FetchTagsForImageQuery : IQuery
    {
        private readonly HImage _image;
        
        public TagSet Result { get; private set; }

        public FetchTagsForImageQuery(HImage image)
        {
            _image = image;
        }

        public void Execute(SQLiteCommand command)
        {
            ISet<Model.Tag> tags = new SortedSet<Model.Tag>();

            command.CommandText
                    = @"SELECT * 
                        FROM Tags
                        where id = @id;";
            command.Parameters.AddWithValue("@id", _image.UUID);

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                tags.Add(dataReader.GetTag());
            }

            Result = new TagSet(tags);
        }
    }
}
