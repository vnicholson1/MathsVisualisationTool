using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of 
 * SolveItException.PlotFunctionException.PlotFunctionArgumentException.InvalidRangeException.InvalidVariableNameInRangeException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if the independent variable is specified in the range argument (1-Y-10 rather than 1-X-10).
    /// </summary>
    public class DependentAndIndependentVariablesWithSameNameException : InvalidVariableNameInRangeException
    {
        public DependentAndIndependentVariablesWithSameNameException(string message) : base(message)
        {
            ErrorCode = "5.1.2.1.0";
        }
    }
}
