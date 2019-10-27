using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MathsVisualisationTool
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
        private string op;
        private Expression right;

        public Operation(Expression left, string op, Expression right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override double Evaluate()
        {
            double x = left.Evaluate();
            double y = right.Evaluate();

            switch(op)
            {
                case "+": return x + y;
                case "-": return x - y;
                case "*": return x * y;
                case "/": return x / y;
            }
            throw new Exception("Unrecognised operator - " + op);
        }
    }

    class Parser
    {

        private List<Token> tokens;
        private Token nextToken = null;
        private int index = 0;
        
        public Parser()
        {

        }

        public double AnalyseTokens(List<Token> tokens)
        {
            this.tokens = tokens;
            getNextToken();

            double value = double.NaN;

            while(nextToken != null)
            {
                SUPPORTED_TOKENS tokenType = nextToken.GetType();

                if(tokenType.Equals(SUPPORTED_TOKENS.INTEGER))
                {
                    value = Convert.ToDouble(nextToken.GetValue());
                } else if(tokenType.Equals(SUPPORTED_TOKENS.DIVISION) || tokenType.Equals(SUPPORTED_TOKENS.MULTIPLICATION))
                {
                    value = divisionAndMultHandle(value);
                } else if (tokenType.Equals(SUPPORTED_TOKENS.PLUS) || tokenType.Equals(SUPPORTED_TOKENS.MINUS))
                {
                    value = plusAndMinusHandle(value);
                }

                getNextToken();
            }

            return value;
        }

        private double divisionAndMultHandle(double value)
        {

            Expression left;
            Expression right;
            string op = nextToken.GetValue();

            //so there is nothing before 
            if (double.IsNaN(value))
            {
                throw new Exception("Integer expected at token position " + (index - 1));
            }

            left = new Constant(value);

            Token peekToken = peek();

            //check what the next token is as it should be either a plus, minus or and int.
            if(peekToken == null 
                || peekToken.GetType().Equals(SUPPORTED_TOKENS.MULTIPLICATION) 
                || peekToken.GetType().Equals(SUPPORTED_TOKENS.DIVISION))
            {
                throw new Exception("Integer expected at token position " + (index + 1));
            } else
            {
                //get the next token
                getNextToken();
                double multiplier = 1;

                //gather the multiplier for situations like 8*-8 = -64 rather than 64.
                while(nextToken.GetType().Equals(SUPPORTED_TOKENS.PLUS) || nextToken.GetType().Equals(SUPPORTED_TOKENS.MINUS))
                {
                    if(nextToken.GetType().Equals(SUPPORTED_TOKENS.PLUS))
                    {
                        multiplier *= 1;
                    } else
                    {
                        multiplier *= -1;
                    }
                    getNextToken();
                }
                
                //Prevent division by zero
                if (Convert.ToDouble(nextToken.GetValue()) == 0 && op == "/")
                {
                    throw new Exception("Cannot divide by zero");
                }

                right = new Constant(Convert.ToDouble(nextToken.GetValue()) * multiplier);

                Expression ne = new Operation(left, op, right);

                return ne.Evaluate();
            }
        }

        private double plusAndMinusHandle(double value)
        {
            Expression left = null;
            Expression right = null;
            string op = nextToken.GetValue();

            //so there is nothing before 
            if (double.IsNaN(value))
            {
                left = new Constant(0);
            } else
            {
                left = new Constant(value);
            }

            Token peekToken = peek();
            double multiplier = 1;

            //Check for invalid syntax and to evaluate situations like 3+-3 as you would want this to evaluate to 3-3 etc.
            while(!(peekToken == null))
            {
                if (peekToken.GetType().Equals(SUPPORTED_TOKENS.PLUS))
                {
                    multiplier *= 1;
                } else if (peekToken.GetType().Equals(SUPPORTED_TOKENS.MINUS))
                {
                    multiplier *= -1;
                } else if (peekToken.GetType().Equals(SUPPORTED_TOKENS.MULTIPLICATION) || peekToken.Equals(SUPPORTED_TOKENS.DIVISION))
                {
                    throw new Exception("Invalid token at position - " + (index + 1));
                } else if(peekToken.GetType().Equals(SUPPORTED_TOKENS.INTEGER))
                {
                    right = new Constant(Convert.ToDouble(peekToken.GetValue()) * multiplier);
                    getNextToken();
                    break;
                }
                getNextToken();
                peekToken = peek();
            }

            if(right == null)
            {
                throw new Exception("Integer expected at position - " + (index));
            }

            Expression ne = new Operation(left, op, right);

            return ne.Evaluate();
        }

        /// <summary>
        /// Private function for getting the next token identified by the lexer.
        /// Sets the nextToken as null if there is no next token.
        /// </summary>
        private void getNextToken()
        {
            if(index < tokens.Count)
            {
                nextToken = tokens[index];
                index++;
            } else
            {
                nextToken = null;
            }
        }

        /// <summary>
        /// Private function to peek at the next token. Returns the next token without
        /// setting nextToken. Returns null if there is no next token.
        /// </summary>
        /// <returns></returns>
        private Token peek()
        {
            if (index < tokens.Count)
            {
                return tokens[index];
            }
            else
            {
                return null;
            }
        }
    }
}
