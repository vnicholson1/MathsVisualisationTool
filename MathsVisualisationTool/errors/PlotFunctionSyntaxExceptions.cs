using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of 
 * SolveItException.PlotFunctionException.PlotFunctionSyntaxException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there is no open bracket token found straight after a plot function name.
    /// </summary>
    public class MissingOpenBracketAfterPlotFunctionNameException : PlotFunctionSyntaxException
    {
        public MissingOpenBracketAfterPlotFunctionNameException(string message) : base(message)
        {
            ErrorCode = "5.0.0";
        }
    }

    // <summary>
    /// Error thrown if the parser unexpectedly reached the end of the input for the plot function. 
    /// </summary>
    public class UnexpectedlyReachedEndOfExpressionException : PlotFunctionSyntaxException
    {
        public UnexpectedlyReachedEndOfExpressionException(string message) : base(message)
        {
            ErrorCode = "5.0.1";
        }
    }
}
