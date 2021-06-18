using System.Collections;
using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Helper.Extensions;

namespace tag_h.Core.SQLGeneration
{
    public class QSelect : QClause, IEnumerable<QField>
    {
        public int Order => 0;

        private List<QField> _fields = new();

        public IEnumerator<QField> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        public string Realise => $"SELECT {this.Select(field => field.Realise).Join(", ")}";
    }
}
