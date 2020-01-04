using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of 
 * SolveItException.PlotFunctionException.PlotFunctionArgumentException.InvalidEquationException
 */
namespace MathsVisualisationTool
{
    /// <summary>
    /// Error thrown if there are too many variables specified in the equation argument.
    /// This software only supports up to 2 variables.
    /// </summary>
    public class TooManyVariablesException : InvalidEquationException
    {
        public TooManyVariablesException(string message) : base(message)
        {
            ErrorCode = "5.1.1.0";
        }
    }

    /// <summary>
    /// Error thrown if the equation specified is too short.
    /// I.e. min equation is "Y=3".
    /// </summary>
    public class TooShortEquationException : InvalidEquationException
    {
        public TooShortEquationException(string message) : base(message)
        {
            ErrorCode = "5.1.1.1";
        }
    }

    /// <summary>
    /// Error thrown if there is no dependent variable specified.
    /// I.e. no "Y=" component.
    /// </summary>
    public class MissingDependentVariableException : InvalidEquationException
    {
        public MissingDependentVariableException(string message) : base(message)
        {
            ErrorCode = "5.1.1.2";
        }
    }

    /// <summary>
    /// Error thrown if the independent variable is the same as the dependent variable e.g. Y=Y^2 would throw this error.
    /// </summary>
    public class DependentAndIndependentVariablesWithSameNameException : InvalidVariableNameInRangeException
    {
        public DependentAndIndependentVariablesWithSameNameException(string message) : base(message)
        {
            ErrorCode = "5.1.1.3";
        }
    }
}
