using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public abstract class Expression
    {
        /// <summary>
        /// Evaluate function that will give the value of the given class.
        /// </summary>
        /// <param name="vars"></param>
        /// <returns></returns>
        public abstract double Evaluate();
    }

    /// <summary>
    /// Class representing a constant i.e. 1,2,3,4,5,6,7,8 etc.
    /// </summary>
    public class Constant : Expression
    {
        private double value;

        public Constant(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Evaluate the value of this class. As it's a constant it's just evaluated as the value.
        /// </summary>
        /// <param name="vars"></param>
        /// <returns></returns>
        public override double Evaluate()
        {
            return value;
        }
    }

    public class Operation : Expression
    {
        private Expression left;
        private readonly Globals.SUPPORTED_TOKENS op;
        private Expression right;

        public Operation(Expression left, Globals.SUPPORTED_TOKENS op, Expression right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override double Evaluate()
        {
            double x = left.Evaluate();
            double y = right.Evaluate();

            switch (op)
            {
                case Globals.SUPPORTED_TOKENS.PLUS: return x + y;
                case Globals.SUPPORTED_TOKENS.MINUS: return x - y;
                case Globals.SUPPORTED_TOKENS.MULTIPLICATION: return x * y;
                case Globals.SUPPORTED_TOKENS.DIVISION: return x / y;
            }
            throw new Exception("Unrecognised operator - " + op);
        }
    }
}
