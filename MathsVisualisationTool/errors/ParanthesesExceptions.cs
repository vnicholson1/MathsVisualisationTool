using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.SyntaxErrorException.ParanthesesException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if no supporting close bracket is found after open bracket.
    /// </summary>
    public class MissingCloseBracketException:SyntaxErrorException
    {
        public MissingCloseBracketException(string message) : base(message)
        {
            ErrorCode = "1.5.0";
        }
    }

    /// <summary>
    /// Error thrown if no corresponding open bracket was found for a closed bracket. 
    /// </summary>
    public class MissingOpenBracketException:SyntaxErrorException
    {
        public MissingOpenBracketException(string message) : base(message)
        {
            ErrorCode = "1.5.1";
        }
    }
}
