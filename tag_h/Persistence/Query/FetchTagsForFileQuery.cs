using System.Collections.Generic;
using System.Data.SQLite;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchTagsForFileQuery : IQuery<IEnumerable<TagState>>
    {
        private readonly HFileState _file;

        public FetchTagsForFileQuery(HFileState file)
        {
            _file = file;
        }

        public IEnumerable<TagState> Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    command.CommandText
                       = @"SELECT ImageTags.tagId, Tags.name 
                       FROM ImageTags INNER JOIN Tags 
                       ON ImageTags.tagId = Tags.id
                       WHERE ImageTags.imageId = @imageId;";
                    command.Parameters.AddWithValue("@imageId", _file.Id);

                    return command.ExecuteReader().GetTags();
                }
            );
        }
    }
}
