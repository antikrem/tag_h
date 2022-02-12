using System.Collections.Generic;

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
                       = @"SELECT FileTags.tagId, Tags.name 
                       FROM FileTags INNER JOIN Tags 
                       ON FileTags.tagId = Tags.id
                       WHERE FileTags.fileId = @fileId;";
                    command.Parameters.AddWithValue("@fileId", _file.Id);

                    return command.ExecuteReader().GetTags();
                }
            );
        }
    }
}
