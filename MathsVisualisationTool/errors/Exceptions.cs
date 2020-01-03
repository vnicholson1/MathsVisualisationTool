using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing the root Exception for this application - SolveItException.
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// A General SolveIt Exception class.
    /// Note: has child classes.
    /// </summary>
    public abstract class SolveItException : Exception
    {
        //For the ErrorCode just get the ErrorCode.
        public string ErrorCode { get; set; }

        public SolveItException(string message) : base(message)
        {
        }
    }
}
