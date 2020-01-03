using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of 
 * SolveItException.SyntaxErrorException.IncorrectPositionException.IncorrectPlotFunctionPositionException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there are items found before the plot function declaration.
    /// </summary>
    public class ItemsBeforePlotFunctionException:IncorrectPlotFunctionPositionException
    {
        public ItemsBeforePlotFunctionException(string message):base(message)
        {
            ErrorCode = "1.2.0.0";
        }
    }

    /// <summary>
    /// Error thrown if there are items found after the plot function declaration.
    /// </summary>
    public class ItemsAfterPlotFunctionException : IncorrectPlotFunctionPositionException
    {
        public ItemsAfterPlotFunctionException(string message) : base(message)
        {
            ErrorCode = "1.2.0.1";
        }
    }
}
