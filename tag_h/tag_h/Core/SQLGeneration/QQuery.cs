using System;
using System.Collections.Generic;
using System.Linq;

using tag_h.Core.Helper.Extensions;

namespace tag_h.Core.SQLGeneration
{
    public class QQuery : QView
    {
        List<QClause> _clauses = new();

        public string Realise => _clauses.Select(clause => clause.Realise).Join(Environment.NewLine);
    }
}
