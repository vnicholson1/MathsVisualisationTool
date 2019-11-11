using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    public class SyntaxErrorException : Exception
    {
        public SyntaxErrorException()
        {

        }

        public SyntaxErrorException(string message)
        : base(message)
        {
        }
    }
}
