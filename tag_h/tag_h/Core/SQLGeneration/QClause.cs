using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tag_h.Core.SQLGeneration
{
    public interface QClause : QExpression
    {
        int Order { get; }
    }

    internal class QClauseComparator : IComparer<QClause>
    {
        private static Type[] ORDER = new Type[]{ typeof(QSelect), typeof(QFrom) };
        public int Compare(QClause x, QClause y)
        {
            return Array.IndexOf(ORDER, x.GetType()).CompareTo(Array.IndexOf(ORDER, y.GetType()));
        }
    }
}
