using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.SyntaxErrorException.InvalidTypeException
 */
namespace MathsVisualisationTool
{

    /// <summary>
    /// Error thrown if there is an incorrect type found before the division operator.
    /// </summary>
    public class InvalidTypeBeforeDivisionOperatorException : InvalidTypeException
    {
        public InvalidTypeBeforeDivisionOperatorException(string message) : base(message)
        {
            ErrorCode = "1.4.0";
        }
    }

    /// <summary>
    /// Error thrown if there is an incorrect type found before the multiplication operator.
    /// </summary>
    public class InvalidTypeBeforeMultiplicationOperatorException : InvalidTypeException
    {
        public InvalidTypeBeforeMultiplicationOperatorException(string message) : base(message)
        {
            ErrorCode = "1.4.1";
        }
    }

    /// <summary>
    /// Error thrown if there is an incorrect type found before the assignment operator.
    /// </summary>
    public class InvalidTypeBeforeAssignmentOperatorException : InvalidTypeException
    {
        public InvalidTypeBeforeAssignmentOperatorException(string message) : base(message)
        {
            ErrorCode = "1.4.2";
        }
    }

    /// <summary>
    /// Error thrown if there is an incorrect type found before the indicies operator.
    /// </summary>
    public class InvalidTypeBeforeIndiciesOperatorException : InvalidTypeException
    {
        public InvalidTypeBeforeIndiciesOperatorException(string message) : base(message)
        {
            ErrorCode = "1.4.3";
        }
    }

    /// <summary>
    /// Error thrown if there is an incorrect type found after the plus operator.
    /// </summary>
    public class InvalidTypeAfterPlusOperatorException : InvalidTypeException
    {
        public InvalidTypeAfterPlusOperatorException(string message) : base(message)
        {
            ErrorCode = "1.4.4";
        }
    }

    /// <summary>
    /// Error thrown if there is an incorrect type found after the minus operator.
    /// </summary>
    public class InvalidTypeAfterMinusOperatorException : InvalidTypeException
    {
        public InvalidTypeAfterMinusOperatorException(string message) : base(message)
        {
            ErrorCode = "1.4.5";
        }
    }

    /// <summary>
    /// Error thrown if there is an incorrect type found after the assignment operator.
    /// </summary>
    public class InvalidTypeAfterAssignmentOperatorException : InvalidTypeException
    {
        public InvalidTypeAfterAssignmentOperatorException(string message) : base(message)
        {
            ErrorCode = "1.4.6";
        }
    }
}
