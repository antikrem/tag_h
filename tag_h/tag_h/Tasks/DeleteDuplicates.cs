using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tag_h.Persistence;

namespace tag_h.Tasks
{
    class DeleteDuplicates : ITask
    {
        public string TaskName => "Deleting Duplicate";

        public void Execute(IImageDatabase database)
        {
            var dbImages = database.FetchAllImages();


        }
    }
}
