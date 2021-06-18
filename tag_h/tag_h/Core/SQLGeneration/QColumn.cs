using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tag_h.Core.SQLGeneration
{
    public class QColumn : QField
    {
        private string _name;

        public QColumn(string name)
        {
            _name = name;
        }

        public string Realise => _name;
    }
}
