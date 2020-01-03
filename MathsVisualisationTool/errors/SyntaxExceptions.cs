using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    public class SyntaxErrorException : SolveItException
    {
        public SyntaxErrorException(string message)
        : base(message)
        {
        }
    }

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


    public class VariableReferenceException : SyntaxErrorException
    {

        public VariableReferenceException(string message)
        : base(message)
        {
            ErrorCode = "67";
        }
    }
}
