using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tag_h.Core.SQLGeneration
{

    public class QWhere : QClause
    {
        private QConditional _condition;

        public string Realise => _condition.Realise;

        public QWhere(QConditional condition)
        {
            _condition = condition;
        }
    }

}
