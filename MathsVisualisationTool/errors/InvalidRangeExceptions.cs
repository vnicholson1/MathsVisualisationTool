using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of 
 * SolveItException.PlotFunctionException.PlotFunctionArgumentException.InvalidRangeException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if the range specified is empty (no 2nd parameter in range argument).
    /// </summary>
    public class EmptyRangeException : InvalidRangeException
    {
        public EmptyRangeException(string message) : base(message)
        {
            ErrorCode = "5.1.2.0";
        }
    }

    /// <summary>
    /// Error thrown if the variable name is incorrect (i.e. it is a variable type but the name is wrong).
    /// Note: has child classes.
    /// </summary>
    public class InvalidVariableNameInRangeException : InvalidRangeException
    {
        public InvalidVariableNameInRangeException(string message) : base(message)
        {
            ErrorCode = "5.1.2.1";
        }
    }

    /// <summary>
    /// Error thrown if there isn't a less than or greater than symbol after the variable name.
    /// </summary>
    public class InvalidTypeAfterVariableNameException : InvalidRangeException
    {
        public InvalidTypeAfterVariableNameException(string message) : base(message)
        {
            ErrorCode = "5.1.2.2";
        }
    }

    /// <summary>
    /// Error thrown if less than and greater than operators are mixed together.
    /// </summary>
    public class MixedRangeOperatorsException : InvalidRangeException
    {
        public MixedRangeOperatorsException(string message) : base(message)
        {
            ErrorCode = "5.1.2.3";
        }
    }

    /// <summary>
    /// Error thrown if anything other than a variable name is found in the variable position.
    /// </summary>
    public class IncorrectTypeInVariablePositionException : InvalidRangeException
    {
        public IncorrectTypeInVariablePositionException(string message) : base(message)
        {
            ErrorCode = "5.1.2.4";
        }
    }

    /// <summary>
    /// Error thrown if the Xmin is greater than or equal to the Xmax argument.
    /// </summary>
    public class XminGreaterThanXmasException : InvalidRangeException
    {
        public XminGreaterThanXmasException(string message) : base(message)
        {
            ErrorCode = "5.1.2.5";
        }
    }
}
