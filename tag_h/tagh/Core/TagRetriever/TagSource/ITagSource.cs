using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tag_h.Model;

namespace tag_h.TagRetriever.TagSource
{
    
    public interface ITagSource
    {

        Task<TagSet> RetrieveTags();
    }
}
