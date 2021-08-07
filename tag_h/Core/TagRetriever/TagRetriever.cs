using System.Collections.Generic;

using tag_h.Core.Model;

namespace tag_h.Core.TagRetriever
{
    public interface ITagRetriever
    {
        List<string> FetchTagValues(HImage image);
    }

    public class TagRetriever : ITagRetriever
    {
        public List<string> FetchTagValues(HImage image)
        {
            throw new System.NotImplementedException();
        }
    }
}
