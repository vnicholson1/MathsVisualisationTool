using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.SyntaxErrorException.
 */
namespace MathsVisualisationTool
{
    
    /// <summary>
    /// Error thrown when someone tries to send an empty string to the interpreter.
    /// </summary>
    public class EmptyInputException : SyntaxErrorException
    {
        public EmptyInputException(string message) : base(message)
        {
            ErrorCode = "1.0";
        }
    }

    /// <summary>
    /// Error thrown when someone tries to input an unrecognised character into the interpreter.
    /// </summary>
    public class UnknownCharacterException : SyntaxErrorException
    {
        public UnknownCharacterException(string message) : base(message)
        {
            ErrorCode = "1.1";
        }
    }

    /// <summary>
    /// Error thrown if a particular token is found in an incorrect position (for example the plot function
    /// has to be the first token if used).
    /// Note: Has child classes.
    /// </summary>
    public abstract class IncorrectPositionException : SyntaxErrorException
    {
        public IncorrectPositionException(string message) : base(message)
        {
            ErrorCode = "1.2";
        }
    }

    /// <summary>
    /// Error thrown if an expression is missing a particular operator for whatever reason.
    /// Note: Has child classes.
    /// </summary>
    public abstract class MissingOperatorException : SyntaxErrorException
    {
        public MissingOperatorException(string message) : base(message)
        {
            ErrorCode = "1.3";
        }
    }

    /// <summary>
    /// Error thrown if an invalid token type is found before a certain operator.
    /// Note: Has child classes.
    /// </summary>
    public abstract class InvalidTypeException : SyntaxErrorException
    {
        public InvalidTypeException(string message) : base(message)
        {
            ErrorCode = "1.4";
        }
    }

    /// <summary>
    /// Error thrown if there is a problem related to the user's use of ( and ) characters.
    /// Note: Has child classes.
    /// </summary>
    public abstract class ParanthesesException : SyntaxErrorException
    {
        public ParanthesesException(string message) : base(message)
        {
            ErrorCode = "1.5";
        }
    }

    /// <summary>
    /// Error thrown if an expression is expected at a particular position but one was not found.
    /// Note: Has child classes.
    /// </summary>
    public abstract class MissingExpressionException : SyntaxErrorException
    {
        public MissingExpressionException(string message) : base(message)
        {
            ErrorCode = "1.6";
        }
    }
}
