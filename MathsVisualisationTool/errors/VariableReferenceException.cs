using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    public class VariableReferenceException : SyntaxErrorException
    {

        public VariableReferenceException(string message)
        : base(message)
        {
            ErrorCode = "2";
        }
    }
}
