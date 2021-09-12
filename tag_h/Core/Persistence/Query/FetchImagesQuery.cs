using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Model;


namespace tag_h.Core.Persistence.Query
{
    class FetchImagesQuery : IQuery
    {
        private readonly ImageQuery _query;

        public List<HImage> Result { get; private set; }

        public FetchImagesQuery(ImageQuery query)
        {
            _query = query;
        }

        public void Execute(ISQLCommandExecutor commandExecutor)
        {
            commandExecutor.ExecuteCommand(
                command =>
                {
                    var commandText
                    = @"SELECT imageId, fileName, fileHash
                        FROM (" +
                        GetInnerQuery() +
                        @"
                        ) WHERE $WHERECLAUSE
                        $LIMIT;";
                    commandText = commandText.Replace("$LIMIT", _query.Maximum != int.MaxValue ? $"LIMIT {_query.Maximum}" : "");
                    commandText = commandText.Replace("$WHERECLAUSE", string.Join(" AND ", BuildWhereClauseParts()));

                    command.CommandText = commandText;

                    Result = command
                        .ExecuteReader()
                        .GetHImages()
                        .ToList();
                }
            );
        }

        private string GetInnerQuery()
        {
            return _query.Included.Any()
                ? @"SELECT Images.id as imageId, Images.fileName as fileName, Images.deleted as deleted, Images.fileHash as fileHash, count(Images.id) as matchedTags
                    FROM Images LEFT JOIN ImageTags
                    ON Images.id == ImageTags.imageId
                    WHERE " + GetIncludedTagClause() + @"
                    GROUP BY Images.id"
                : @"SELECT id as imageId, fileName as fileName, deleted, fileHash
                    FROM Images";
        }

        private string GetIncludedTagClause() => string.Join(" or ", BuildInnerTagClauseParts());

        private IEnumerable<string> BuildInnerTagClauseParts() => _query.Included.Select(tag => $"ImageTags.tagId == {tag}");

        private IEnumerable<string> BuildWhereClauseParts()
        {
            yield return " deleted = 0";

            if (_query.Id > 0)
                yield return $"imageId = {_query.Id}";

            if (_query.Location != null)
                yield return $"fileName = '{_query.Location}'";

            if (_query.Location != null)
                yield return $"fileName = '{_query.Location}'";

            if (_query.Included.Any())
                yield return $"matchedTags == {_query.Included.Count()}";

            if (_query.ImageHash != null)
                yield return $"fileHash == '{_query.ImageHash.FileHash}'";
        }
    }

}
