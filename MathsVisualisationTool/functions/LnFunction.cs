using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    class LnFunction : MathFunction
    {
        /// <summary>
        /// Constructor is the same for the TrigFunction class.
        /// </summary>
        /// <param name="equation"></param>
        public LnFunction(List<Token> equation, int index, bool hasSecondParameter)
            : base(equation, index, hasSecondParameter)
        {
        }

        protected override double Evaluate()
        {
            return Math.Log(Convert.ToDouble(firstParameter.GetValue()));
        }
    }
}
