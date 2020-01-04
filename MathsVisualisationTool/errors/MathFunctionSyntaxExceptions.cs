using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.MathFunctionException.MathFunctionSyntaxException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there is no open bracket token found straight after a function name.
    /// </summary>
    public class MissingOpenBracketAfterFunctionNameException:MathFunctionSyntaxException
    {
        public MissingOpenBracketAfterFunctionNameException(string message) : base(message)
        {
            ErrorCode = "4.0.0";
        }
    }

    /// <summary>
    /// Error thrown if there is no close bracket found for the function declaration. 
    /// </summary>
    public class MissingCloseBracketExceptionForFunction : MathFunctionSyntaxException
    {
        public MissingCloseBracketExceptionForFunction(string message): base(message)
        {
            ErrorCode = "4.0.1";
        }
    }
}
