using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of 
 * SolveItException.MathFunctionException.MathFunctionArgumentExceptions
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there are too many arguments in the math function.
    /// </summary>
    public class TooManyArgumentsException : MathFunctionArgumentException
    {
        public TooManyArgumentsException(string message) : base(message)
        {
            ErrorCode = "4.1.0";
        }
    }

    /// <summary>
    /// Error thrown if there are too little arguments in the math function.
    /// </summary>
    public class TooLittleArgumentsException : MathFunctionArgumentException
    {
        public TooLittleArgumentsException(string message) : base(message)
        {
            ErrorCode = "4.1.1";
        }
    }
}
