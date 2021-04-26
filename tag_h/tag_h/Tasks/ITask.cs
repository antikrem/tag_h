using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Tasks
{
    interface ITask
    {
        void Execute(Persistence.ImageDatabase database);
    }
}
