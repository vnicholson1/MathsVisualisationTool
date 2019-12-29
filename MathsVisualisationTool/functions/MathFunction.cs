using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    abstract class MathFunction
    {
        //The value that is enclosed in brackets.
        protected Token firstParameter;
        //The second parameter (if it exists)
        protected Token secondParameter = null;
        //For recording the number of tokens that take up the sin function.
        //This includes from the sin token all the way to the final closing bracket ).
        protected int lengthOfExpression = 0;
        //For storing the new modified equation.
        protected List<Token> newEquation;
        //For stating whether a function has a second parameter has some math functions do but others don't.
        protected bool hasSecondParameter;
        //for storing the result of this function.
        protected Token result;

        /// <summary>
        /// Constructor for the TrigFunction class. Takes in an equation list and a current index of 
        /// where the sin/cos/tan token has been found.
        /// </summary>
        /// <param name="equation"></param>
        /// <param name="index"></param>
        public MathFunction(List<Token> equation, int index, bool hasSecondParameter)
        {
            this.hasSecondParameter = hasSecondParameter;

            checkSyntax(equation,index);

            result = new Token(Globals.SUPPORTED_TOKENS.CONSTANT,Convert.ToString(Evaluate()));

            modifyNewEquation(equation,index);
        }

        /// <summary>
        /// Function to check that the syntax is all okay. It also gets the value of the expression
        /// enclosed in the sin/cos/tan function's brackets. 
        /// </summary>
        /// <param name="equation"></param>
        /// <param name="index"></param>
        private void checkSyntax(List<Token> equation, int index)
        {
            index++;
            //Next token after the sin must be an open bracket.
            if(equation[index].GetType() != Globals.SUPPORTED_TOKENS.OPEN_BRACKET)
            {
                throw new SyntaxErrorException("Open Bracket expected at token position - " + index + ".");
            }

            int bracketLevel = 0;
            List<Token> enclosingFunction = new List<Token>();
            //Get the equation enclosed in the brackets.
            while(true)
            {
                enclosingFunction.Add(equation[index]);
                if(equation[index].GetType() == Globals.SUPPORTED_TOKENS.OPEN_BRACKET)
                {
                    bracketLevel++;
                } else if (equation[index].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
                {
                    bracketLevel--;
                }

                if(bracketLevel == 0)
                {
                    break;
                }
                if (index == equation.Count - 1)
                {
                    break;
                }
                index++;
            }

            if(bracketLevel != 0)
            {
                throw new SyntaxErrorException("No closing bracket found for corresponding open bracket.");
            }

            //Read in the variables
            Hashtable vars = VariableFileHandle.getVariables();
            //create a parser object
            Parser p = new Parser(vars);
            //Analyse the syntax of the enclosing function using the parser object.
            double result = p.AnalyseTokens(enclosingFunction);

            firstParameter = new Token(Globals.SUPPORTED_TOKENS.CONSTANT, Convert.ToString(result));
            lengthOfExpression = enclosingFunction.Count + 1;
        }

        /// <summary>
        /// Function to remove the sin token from the equation and replace it with the result of the function.
        /// </summary>
        /// <param name="equation"></param>
        /// <param name="index"></param>
        private void modifyNewEquation(List<Token> equation, int index)
        {
            equation.RemoveRange(index, lengthOfExpression);
            equation.Insert(index, result);

            newEquation = equation;
        }

        public List<Token> getNewEquation()
        {
            return newEquation;
        }

        protected abstract double Evaluate();
    }
}
