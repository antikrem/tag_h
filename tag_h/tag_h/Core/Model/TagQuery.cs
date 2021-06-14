#nullable enable

namespace tagh.Core.Model
{
    public record TagQuery
    {
        public TagSet Included { get; set; } = TagSet.Empty;
        public TagSet Excluded { get; set; } = TagSet.Empty;
        public int Maximum { get; set; } = int.MaxValue;

        public static TagQuery All = new TagQuery();
    };
}
