using System;
using System.Linq;

using tagh.Core.Helper.Extensions;
using tagh.Core.Model;
using tagh.Core.Persistence;

namespace tagh.Core.Tasks
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
