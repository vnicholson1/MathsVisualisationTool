using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    public class DivideByZeroException : Exception
    {
        public DivideByZeroException()
        {
        }

        public DivideByZeroException(string message)
        : base(message)
        {
        }
    }
}
