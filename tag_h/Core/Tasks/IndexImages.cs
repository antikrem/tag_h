using System.Linq;

using tag_h.Core.Helper.Extensions;
using tag_h.Core.Model;
using tag_h.Core.Persistence;


namespace tag_h.Core.Tasks
{
    class IndexImages : ITask
    {
        public string TaskName => "Indexing all Images";

        public void Execute(IHImageRepository imageRepository)
        {
            using (var images = imageRepository.FetchImages(TagQuery.All))
            {
                var unhashedImages = images
                    .Where(x => x.IsHashableFormat())
                    .Where(y => y.Hash == null);

                unhashedImages.ForEach(x => x.Index());
            }
        }
    }
}
