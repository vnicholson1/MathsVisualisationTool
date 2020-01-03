using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.MathFunctionException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there are any syntax errors related to the mathematical functions.
    /// Note: has child classes.
    /// </summary>
    public abstract class MathFunctionSyntaxException:MathFunctionException
    {
        public MathFunctionSyntaxException(string message) : base(message)
        {
            ErrorCode = "4.0";
        } 
    }

    /// <summary>
    /// Error thrown if there is an error with the number of arguments passed into the math function.
    /// Note: has child classes.
    /// </summary>
    public abstract class MathFunctionArgumentException: MathFunctionException
    {
        public MathFunctionArgumentException(string message) : base(message)
        {
            ErrorCode = "4.1";
        }
    }
}
