using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.PlotFunctionException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there are any syntax errors related to the plot function.
    /// Note: has child classes.
    /// </summary>
    public abstract class PlotFunctionSyntaxException:PlotFunctionException
    {
        public PlotFunctionSyntaxException(string message):base(message)
        {
            ErrorCode = "5.0";
        }
    }

    /// <summary>
    /// Error thrown if there is an error with the number of arguments passed into the plot function.
    /// Note: has child classes.
    /// </summary>
    public abstract class PlotFunctionArgumentException:PlotFunctionException
    {
        public PlotFunctionArgumentException(string message) : base(message)
        {
            ErrorCode = "5.1";
        }
    }
}
