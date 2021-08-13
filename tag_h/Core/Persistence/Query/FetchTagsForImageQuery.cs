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
                    = @"SELECT ImageTags.tagId, Tags.name 
                        FROM ImageTags INNER JOIN Tags 
                        ON ImageTags.tagId = Tags.id
                        WHERE ImageTags.imageId = @imageId;";
            command.Parameters.AddWithValue("@imageId", _image.Id);

            Result = command.ExecuteReader().GetTags();
        }
    }
}
