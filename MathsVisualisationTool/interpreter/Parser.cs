using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{ 
    class Parser
    {
        //List of tokens gathered by the lexer.
        private List<Token> tokens;
        //Variable to store the current token that the parser is analysing.
        private Token nextToken = null;
        //the index of the NEXT token to be analysed when getNextToken() is called.
        private int index = 0;
        //bracketLevel is for storing the bracket level of an expression
        // i.e. ( will set the bracket Level to 1 but a ) will set it back to 0 again.
        private int bracketLevel = 0;
        
        public Parser()
        {
            
        }

        /// <summary>
        /// Method called by the interpreter to analyse the set of tokens gathered by the lexer.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns> the value of the given expression.</returns>
        public double AnalyseTokens(List<Token> tokens)
        {
            this.tokens = tokens;
            getNextToken();

            double value = analyseExpressions(double.NaN);
            return value;
        }


        public double analyseExpressions(double value)
        {
            while (nextToken != null)
            {
                SUPPORTED_TOKENS tokenType = nextToken.GetType();
                if (tokenType == SUPPORTED_TOKENS.INTEGER)
                {
                    value = Convert.ToDouble(nextToken.GetValue());
                    getNextToken();
                }
                else if (tokenType == SUPPORTED_TOKENS.DIVISION || tokenType == SUPPORTED_TOKENS.MULTIPLICATION)
                {
                    value = divisionAndMultHandle(value);
                    getNextToken();
                }
                else if (tokenType == SUPPORTED_TOKENS.PLUS || tokenType == SUPPORTED_TOKENS.MINUS)
                {
                    value = plusAndMinusHandle(value);
                }
                else if (tokenType == SUPPORTED_TOKENS.OPEN_BRACKET)
                {
                    bracketLevel++;
                    getNextToken();
                    value = analyseExpressions(value);
                } else if (tokenType == SUPPORTED_TOKENS.CLOSE_BRACKET)
                {
                    //Cannot have a closing bracket without an open bracket.
                    if(bracketLevel == 0)
                    {
                        throw new Exception("Unrecognised character ')' at token position " + index);
                    }
                    bracketLevel--;
                    getNextToken();
                    return value;
                }
            }
            return value;
        }

        /// <summary>
        /// Function for handling division and multiplication when that operator has been found.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>  Returns the resuling value of that operator. </returns>
        private double divisionAndMultHandle(double value)
        {

            Expression left;
            Expression right;
            SUPPORTED_TOKENS op = nextToken.GetType();

            //so there is nothing before 
            if (double.IsNaN(value))
            {
                throw new Exception("Integer expected at token position " + (index - 1));
            }

            //Find the left and right hand side of the expression.
            left = new Constant(value);

            Token peekToken = peek();

            //check what the next token is as it should be either a plus, minus or and int.
            if(peekToken == null || 
                !(peekToken.GetType() == SUPPORTED_TOKENS.PLUS) 
                && !(peekToken.GetType() == SUPPORTED_TOKENS.MINUS)
                && !(peekToken.GetType() == SUPPORTED_TOKENS.INTEGER)
                && !(peekToken.GetType() == SUPPORTED_TOKENS.OPEN_BRACKET))
            {
                throw new Exception("Integer expected at token position " + index);
            } else
            {
                //get the next token
                getNextToken();
            
                double multiplier = 1;

                //gather the multiplier for situations like 8*-8 = -64 rather than 64.
                while(nextToken.GetType() == SUPPORTED_TOKENS.PLUS || nextToken.GetType() == SUPPORTED_TOKENS.MINUS)
                {
                    if(nextToken.GetType() == SUPPORTED_TOKENS.PLUS)
                    {
                        multiplier *= 1;
                    } else
                    {
                        multiplier *= -1;
                    }
                    getNextToken();
                }

                double rightValue = 0.0;

                //If a bracket has been found then analyse the expression inside the bracket first.
                if (nextToken.GetType() == SUPPORTED_TOKENS.OPEN_BRACKET)
                {
                    bracketLevel++;
                    getNextToken();
                    rightValue = multiplier*analyseExpressions(value);
                } else
                {
                    rightValue = multiplier * Convert.ToDouble(nextToken.GetValue());
                }

                right = new Constant(rightValue);

                //Prevent division by zero
                if (rightValue == 0 && op == SUPPORTED_TOKENS.DIVISION)
                {
                    throw new Exception("Cannot divide by zero");
                }

                Expression ne = new Operation(left, op, right);

                return ne.Evaluate();
            }
        }

        /// <summary>
        /// Function for handling the plus and minus operator when one has been found.
        /// </summary>
        /// <param name="value"></param>
        /// <returns> Returns the new value of the resulting calculation. </returns>
        private double plusAndMinusHandle(double value)
        {
            Expression left = null;
            Expression right = null;
            SUPPORTED_TOKENS op = nextToken.GetType();

            //so there is nothing before as a situation like '+3' is allowed in the language. 
            if (double.IsNaN(value))
            {
                left = new Constant(0);
            } else
            {
                left = new Constant(value);
            }

            Token peekToken = peek();
            double multiplier = 1;

            while(!(peekToken == null))
            {
                if(peekToken.GetType() == SUPPORTED_TOKENS.INTEGER)
                {
                    //found a number so stop.
                    right = new Constant(Convert.ToDouble(peekToken.GetValue()) * multiplier);
                    //2 next token calls to skip to the next token after the integer token.
                    getNextToken();
                    getNextToken();
                    break;
                } else if (peekToken.GetType() == SUPPORTED_TOKENS.OPEN_BRACKET)
                {
                    getNextToken();
                    bracketLevel++;
                    right = new Constant(analyseExpressions(value));
                    break;
                } else 
                {
                    throw new Exception("Invalid token at position - " + index);
                }
            }

            //so if there is no right hand expression.
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
