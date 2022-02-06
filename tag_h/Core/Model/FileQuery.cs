using tag_h.Persistence;

namespace tag_h.Core.Model
{

    public record FileQuery
    {
        public TagSet Included { get; set; } = TagSet.Empty;
        public TagSet Excluded { get; set; } = TagSet.Empty;
        public string? Location { get; set; }
        public int Maximum { get; set; } = int.MaxValue;
        public int Id { get; set; } = -1;
        public FileHash? ImageHash { get; set; }

        public static FileQuery All => new FileQuery();
    };
}
