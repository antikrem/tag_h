namespace tag_h.Core.Model
{
    public interface ITagQuery
    {

    }

    public class AllImagesTagQuery
    {


    }

    public record TagQuery
    {
        public TagSet Included { get; set; } = TagSet.Empty;
        public TagSet Excluded { get; set; } = TagSet.Empty;
        public int Maximum { get; set; } = int.MaxValue;
        public int UUID { get; set; } = -1;

        public static TagQuery All => new TagQuery();
    };
}
