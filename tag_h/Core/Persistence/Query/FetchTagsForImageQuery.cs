using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
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
                        where imageId = @id;";
            command.Parameters.AddWithValue("@id", _image.Id);

            Result = command.ExecuteReader().GetTags();
        }
    }
}
