using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;
using MathFunctionsFSharp;

namespace MathsVisualisationTool
{
    class FactorialFunction : MathFunction
    {
        /// <summary>
        /// Constructor is the same for the TrigFunction class.
        /// </summary>
        /// <param name="equation"></param>
        public FactorialFunction(List<Token> equation, int index, bool hasSecondParameter)
            : base(equation, index, hasSecondParameter)
        {
        }

        protected override double Evaluate()
        {
            return MathFunctions.factorial((int) Math.Round(Convert.ToDouble(firstParameter.GetValue())));
        }
    }
}
