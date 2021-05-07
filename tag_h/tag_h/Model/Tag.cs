using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tag_h.Model
{
    public class Tag
    {
        public string Value { get; }
        
        public Tag(string value)
        {
            Value = value;
            Value.ToString();
        }

        //
        // Summary:
        //     Returns this instance of System.String; no actual conversion is performed.
        //
        // Returns:
        //     The current string.
        public override String ToString()
        {
            return Value;
        }
    }
}
