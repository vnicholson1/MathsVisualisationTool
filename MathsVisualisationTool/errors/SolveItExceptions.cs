using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// General Syntax Error Exception.
    /// Note: Has child classes.
    /// </summary>
    public abstract class SyntaxErrorException : SolveItException
    {
        public SyntaxErrorException(string message)
        : base(message)
        {
            ErrorCode = "1";
        }
    }

    /// <summary>
    /// Error thrown if a variable is being used but no value has been assigned to it yet.
    /// </summary>
    public class VariableReferenceException : SolveItException
    {
        public VariableReferenceException(string message) : base(message)
        {
            ErrorCode = "2";
        }
    }

    /// <summary>
    /// Error thrown if an error has occured with the 'clear' command.
    /// </summary>
    public class ClearCommandException : SolveItException
    {
        public ClearCommandException(string message): base(message)
        {
            ErrorCode = "3";
        }
    }

    /// <summary>
    /// Error throwns related to the maths functions.
    /// Note: has child classes.
    /// </summary>
    public abstract class MathFunctionException : SolveItException
    {
        public MathFunctionException(string message): base(message)
        {
            ErrorCode = "4";
        }
    }

    /// <summary>
    /// Error throwns related to the plot function.
    /// Note: has child classes.
    /// </summary>
    public abstract class PlotFunctionException : SolveItException
    {
        public PlotFunctionException(string message): base(message)
        {
            ErrorCode = "5";
        }
    }

    /// <summary>
    /// For errors that cause bugs in the interpreter that are not covered by the list of exceptions.
    /// </summary>
    public class UnknownErrorException : SolveItException
    {
        public UnknownErrorException(string message) : base(message)
        {
            ErrorCode = "6";
        }
    }
}
