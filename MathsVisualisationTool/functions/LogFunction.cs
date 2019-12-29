using DataDomain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    class LogFunction : MathFunction
    {
        /// <summary>
        /// Constructor is the same for the TrigFunction class.
        /// </summary>
        /// <param name="equation"></param>
        public LogFunction(List<Token> equation, int index, bool hasSecondParameter)
            : base(equation, index, hasSecondParameter)
        {
        }

        protected override double Evaluate()
        {
            return Math.Log(Convert.ToDouble(firstParameter.GetValue()),Convert.ToDouble(secondParameter.GetValue()));
        }
    }
}
