using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.SyntaxErrorException.MissingExpressionException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if no expression can be found after an operator.
    /// </summary>
    public class MissingExpressionAfterOperatorException : SyntaxErrorException
    {
        public MissingExpressionAfterOperatorException(string message) : base(message)
        {
            ErrorCode = "1.6.0";
        }
    }

    /// <summary>
    /// Error thrown if no expression can be found inbetween a pair of brackets.
    /// </summary>
    public class MissingExpressionBetweenBracketsException : SyntaxErrorException
    {
        public MissingExpressionBetweenBracketsException(string message) : base(message)
        {
            ErrorCode = "1.6.1";
        }
    }
}
