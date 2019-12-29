using DataDomain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    class SinFunction : MathFunction
    {
        /// <summary>
        /// Constructor is the same for the TrigFunction class.
        /// </summary>
        /// <param name="equation"></param>
        public SinFunction(List<Token> equation,int index,bool hasSecondParameter)
            :base(equation,index,hasSecondParameter)
        {
        }

        protected override double Evaluate()
        {
            return Math.Sin(Convert.ToDouble(firstParameter.GetValue()));
        }
    }
}
