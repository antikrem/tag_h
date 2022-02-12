using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Model;
using tag_h.Persistence;
using tag_h.Persistence.Model;

namespace tag_h.Core.Persistence.Query
{
    class FetchFilesQuery : IQuery<List<HFileState>>
    {
        private readonly FileQuery _query;

        public FetchFilesQuery(FileQuery query)
        {
            _query = query;
        }

        public List<HFileState> Execute(ISQLCommandExecutor commandExecutor)
        {
            return commandExecutor.ExecuteCommand(
                command =>
                {
                    var commandText
                    = @"SELECT fileId, fileName, fileHash
                        FROM (" +
                        GetInnerQuery() +
                        @"
                        ) WHERE $WHERECLAUSE
                        $LIMIT;";
                    commandText = commandText.Replace("$LIMIT", _query.Maximum != int.MaxValue ? $"LIMIT {_query.Maximum}" : "");
                    commandText = commandText.Replace("$WHERECLAUSE", string.Join(" AND ", BuildWhereClauseParts()));

                    command.CommandText = commandText;

                    return command
                        .ExecuteReader()
                        .GetHFiles()
                        .ToList();
                }
            );
        }

        private string GetInnerQuery()
        {
            return _query.Included.Any()
                ? @"SELECT Files.id as fileId, Files.fileName as fileName, Files.deleted as deleted, Files.fileHash as fileHash, count(Files.id) as matchedTags
                    FROM Files LEFT JOIN FileTags
                    ON Files.id == FilesTag.fileId
                    WHERE " + GetIncludedTagClause() + @"
                    GROUP BY Files.id"
                : @"SELECT id as fileId, fileName as fileName, deleted, fileHash
                    FROM Files";
        }

        private string GetIncludedTagClause() => string.Join(" or ", BuildInnerTagClauseParts());

        private IEnumerable<string> BuildInnerTagClauseParts() => _query.Included.Select(tag => $"FileTags.tagId == {tag}");

        private IEnumerable<string> BuildWhereClauseParts()
        {
            yield return " deleted = 0";

            if (_query.Id > 0)
                yield return $"fileId = {_query.Id}";

            if (_query.Location != null)
                yield return $"fileName = '{_query.Location}'";

            if (_query.Location != null)
                yield return $"fileName = '{_query.Location}'";

            if (_query.Included.Any())
                yield return $"matchedTags == {_query.Included.Count()}";

            if (_query.ImageHash != null)
                yield return $"fileHash == '{_query.ImageHash.DataHash}'";
        }
    }

}
