using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using tag_h.Persistence;

namespace tag_h.Tasks
{
    public interface ITask
    {
        string TaskName { get; }

        void Execute(IImageDatabase database);
    }
}
