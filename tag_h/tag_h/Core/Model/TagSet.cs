using System.Collections;
using System.Collections.Generic;

namespace tagh.Core.Model
{
    public class TagSet : IEnumerable<Tag>
    {
        private readonly ISet<Tag> _tags;

        private TagSet()
        {
            _tags = new SortedSet<Tag>();
        }

        public TagSet(IEnumerable<Tag> tags)
        {
            _tags = new SortedSet<Tag>(tags);
        }

        public IEnumerator<Tag> GetEnumerator()
        {
            return _tags.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _tags.GetEnumerator();
        }

        static public TagSet Empty => new TagSet();
    }
}
