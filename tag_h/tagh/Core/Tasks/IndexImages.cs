using System;
using System.Linq;

using tag_h.Helper.Extensions;
using tag_h.Model;
using tag_h.Persistence;

namespace tag_h.Tasks
{
    class IndexImages : ITask
    {
        public string TaskName => "Indexing all Images";

        public void Execute(IHImageRepository imageRepository)
        {
            using (var images = imageRepository.FetchAllImages())
            {
                var unhashedImages = images
                    .Where(x => x.IsHashableFormat())
                    .Where(y => y.Hash == null);

                unhashedImages.ForEach(x => x.Index());
            }
        }
    }
}
