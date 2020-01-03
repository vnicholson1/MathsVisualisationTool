using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.SyntaxErrorException.MissingOperatorException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if a variable is found straight after a constant.
    /// E.g. 2x, when the correct input is 2*x.
    /// </summary>
    public class VariableAfterConstantException:SyntaxErrorException
    {
        public VariableAfterConstantException(string message):base(message)
        {
            ErrorCode = "1.3.0";
        }
    }

    /// <summary>
    /// Error thrown if a const is found straight after a constant.
    /// E.g. 2Pi, when the correct input is 2*Pi.
    /// </summary>
    public class ConstantAfterConstantException : SyntaxErrorException
    {
        public ConstantAfterConstantException(string message) : base(message)
        {
            ErrorCode = "1.3.1";
        }
    }
}
