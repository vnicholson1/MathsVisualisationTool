using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of 
 * SolveItException.PlotFunctionException.PlotFunctionArgumentException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there is only one argument found in plot function declaration.
    /// </summary>
    public class MissingRangeArgumentException : PlotFunctionArgumentException
    {
        public MissingRangeArgumentException(string message) : base(message)
        {
            ErrorCode = "5.1.0";
        }
    }

    /// <summary>
    /// Error thrown if there is something wrong with the equation argument.
    /// Note: has child classes.
    /// </summary>
    public abstract class InvalidEquationException : PlotFunctionArgumentException
    {
        public InvalidEquationException(string message) : base(message)
        {
            ErrorCode = "5.1.1";
        }
    }

    /// <summary>
    /// Error thrown if there is something wrong with the range argument.
    /// Note: has child classes.
    /// </summary>
    public abstract class InvalidRangeException: PlotFunctionArgumentException
    {
        public InvalidRangeException(string message) : base(message)
        {
            ErrorCode = "5.1.2";
        }
    }
}
