using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.SyntaxErrorException.IncorrectPositionException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown for if the plot function is found in an incorrect position.
    /// Note: has child classes.
    /// </summary>
    /// <param name="message"></param>
    public abstract class IncorrectPlotFunctionPositionException:IncorrectPositionException
    {
        public IncorrectPlotFunctionPositionException(string message):base(message)
        {
            ErrorCode = "1.2.0";
        }
    }

    /// <summary>
    /// Error thrown for if the assignment operator is found in an incorrect position.
    /// Assignment operator can only be after a VARIABLE_NAME token.
    /// </summary>
    public class IncorrectAssignmentOperatorPositionException:IncorrectPositionException
    {
        public IncorrectAssignmentOperatorPositionException(string message):base(message)
        {
            ErrorCode = "1.2.1";
        }
    }

    /// <summary>
    /// Error thrown for if the multiplication operator is found in an incorrect position.
    /// Usually thrown if someone tries to write the expression "*3" which isn't valid.
    /// </summary>
    public class IncorrectMultiplicationOperatorPositionException : IncorrectPositionException
    {
        public IncorrectMultiplicationOperatorPositionException(string message) : base(message)
        {
            ErrorCode = "1.2.2";
        }
    }

    /// <summary>
    /// Error thrown for if the division operator is found in an incorrect position.
    /// </summary>
    public class IncorrectDivisionOperatorPositionException : IncorrectPositionException
    {
        public IncorrectDivisionOperatorPositionException(string message) : base(message)
        {
            ErrorCode = "1.2.3";
        }
    }

    /// <summary>
    /// Error thrown for if the indicies operator is found in an incorrect position.
    /// </summary>
    public class IncorrectIndiciesOperatorPositionException : IncorrectPositionException
    {
        public IncorrectIndiciesOperatorPositionException(string message) : base(message)
        {
            ErrorCode = "1.2.4";
        }
    }

    /// <summary>
    /// Error thrown for if a close bracket character is found in an incorrect position.
    /// </summary>
    public class IncorrectCloseBracketPositionException : IncorrectPositionException
    {
        public IncorrectCloseBracketPositionException(string message) : base(message)
        {
            ErrorCode = "1.2.5";
        }
    }

    /// <summary>
    /// Error thrown if a less than operator is found outside the range argument of the plot function.
    /// </summary>
    public class IncorrectLessThanOperatorPositionException : IncorrectPositionException
    {
        public IncorrectLessThanOperatorPositionException(string message) : base(message)
        {
            ErrorCode = "1.2.6";
        }
    }

    /// <summary>
    /// Error thrown if a greater than operator is found outside the range argument of the plot function.
    /// </summary>
    public class IncorrectGreaterThanOperatorPositionException : IncorrectPositionException
    {
        public IncorrectGreaterThanOperatorPositionException(string message) : base(message)
        {
            ErrorCode = "1.2.7";
        }
    }
}
