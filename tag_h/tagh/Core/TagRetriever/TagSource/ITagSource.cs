using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagh.Core.Model;

namespace tagh.Core.TagRetriever.TagSource
{

    public interface ITagSource
    {

        Task<TagSet> RetrieveTags();
    }
}
