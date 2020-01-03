using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if a variable is being used but no value has been assigned to it yet.
    /// </summary>
    class VariableReferenceException:SyntaxErrorException
    {
        public VariableReferenceException(string message):base(message)
        {
            ErrorCode = "2";
        }
    }
}
