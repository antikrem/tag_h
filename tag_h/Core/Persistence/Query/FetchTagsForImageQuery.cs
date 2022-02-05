using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchTagsForImageQuery : IQuery<TagSet>
    {
        private readonly HImage _image;

        public FetchTagsForImageQuery(HImage image)
        {
            _image = image;
        }

        public TagSet Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                       = @"SELECT ImageTags.tagId, Tags.name 
                        FROM ImageTags INNER JOIN Tags 
                        ON ImageTags.tagId = Tags.id
                        WHERE ImageTags.imageId = @imageId;";
                    command.Parameters.AddWithValue("@imageId", _image.Id);

                    return command.ExecuteReader().GetTags();
                }
            );
        }
    }
}
