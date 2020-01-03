using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    /// <summary>
    /// Make it abstract because you shouldn't initialise a SolveItException.
    /// </summary>
    public class SolveItException : Exception
    {
        //For the ErrorCode just get the ErrorCode.
        public string ErrorCode { get; set; }

        public SolveItException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// For errors that cause bugs in the interpreter that are not covered by the list of exceptions.
    /// </summary>
    public class UnknownErrorException : SolveItException
    {
        public UnknownErrorException(string message) : base(message)
        {
            ErrorCode = "5.0";
        }
    }
}
