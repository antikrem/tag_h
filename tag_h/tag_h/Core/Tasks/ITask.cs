using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tagh.Core.Persistence;

namespace tagh.Core.Tasks
{
    public interface ITask
    {
        string TaskName { get; }

        void Execute(IHImageRepository imageRepository);
    }
}
