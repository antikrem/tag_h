using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Model
{
    public class TagSet : IEnumerable<Tag>
    {
        private readonly ISet<Tag> _tags; 

        public TagSet()
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
    }
}
