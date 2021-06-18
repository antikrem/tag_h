using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tag_h.Core.SQLGeneration
{
    public class QTable : QView
    {
        private readonly string _name;

        public QTable(string name)
        {
            _name = name;
        }

        public string Realise => _name;
    }
}
