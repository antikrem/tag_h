using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tag_h.Core.SQLGeneration
{
    public class QFrom : QClause
    {
        public int Order => 2;

        private readonly QView _view;

        public QFrom(QView view)
        {
        _view = view;
        }

        public string Realise => $"FROM {_view.Realise}";
    }
}
