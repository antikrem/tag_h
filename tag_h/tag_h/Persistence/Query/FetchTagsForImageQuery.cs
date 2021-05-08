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
            ISet<Tag> tags = new SortedSet<Tag>();

            command.CommandText
                    = @"SELECT tag 
                        FROM ImageTags
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
