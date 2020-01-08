using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.UIErrorException
 */
namespace MathsVisualisationTool
{

    /// <summary>
    /// Error thrown if there are any syntax errors related to the plot function.
    /// Note: has child classes.
    /// </summary>
    public class EmptyVarSaveException : UIErrorException
    {
        public EmptyVarSaveException(string message) : base(message)
        {
            ErrorCode = "7.0";
        }
    }

    /// <summary>
    /// Error thrown if there are any problems loading in the variable file.
    /// </summary>
    public class ErrorLoadingVariableFileException : UIErrorException
    {
        public ErrorLoadingVariableFileException(string message) : base(message)
        {
            ErrorCode = "7.1";
        }
    }
}
