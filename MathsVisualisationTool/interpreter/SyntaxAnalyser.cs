using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    class SyntaxAnalyser
    {

        private List<Token> gatheredTokens;

        /// <summary>
        /// Syntax analyser's job is to check whether the input from the user is valid and
        /// if not throw an appropriate error message.
        /// </summary>
        /// <param name="tokens"></param>
        public SyntaxAnalyser(List<Token> tokens)
        {
            gatheredTokens = tokens;
        }

        /// <summary>
        /// Method to launch the syntax analyser to check if it is all okay.
        /// </summary>
        public void checkTokens()
        {
            int bracketLevel = 0;

            if(gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION 
                || gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.DIVISION
                || gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET
                || gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.INDICIES)
            {
                throw new SyntaxErrorException("Expression cannot start with a multiplication, division, indicies or a closing bracket.");
            }

            Globals.SUPPORTED_TOKENS previousToken = Globals.SUPPORTED_TOKENS.WHITE_SPACE;

            for(int i=0;i<gatheredTokens.Count;i++)
            {
                switch (gatheredTokens[i].GetType())
                {
                    case Globals.SUPPORTED_TOKENS.CONSTANT:
                        //Prevent cases like 2e as you want the user to put 2*e.
                        if(i+1 != gatheredTokens.Count)
                        {
                            if (gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.VARIABLE_NAME)
                            {
                                throw new SyntaxErrorException("Cannot have variable name straight after constant. Did you mean " +
                                    gatheredTokens[i].GetValue() + "*" + gatheredTokens[(i + 1)].GetValue() + "?");
                            }
                        }

                        //Check that only one decimal place has been added or no "." cases
                        int count = gatheredTokens[i].GetValue().Count(x => x == '.');

                        if(count > 1)
                        {
                            throw new SyntaxErrorException("Cannot have more than one decimal point in number.");
                        }

                        if (gatheredTokens[i].GetValue() == ".")
                        {
                            throw new SyntaxErrorException("Unrecognised token - '.'.");
                        }

                        break;

                    case Globals.SUPPORTED_TOKENS.OPEN_BRACKET:
                        bracketLevel++;
                        if(i+1 == gatheredTokens.Count)
                        {
                            throw new SyntaxErrorException("Expression cannot finish with an open bracket.");
                        }

                        //Remove the ability to add ()
                        if(gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
                        {
                            throw new SyntaxErrorException("Enclosing brackets must contain an expression.");
                        }

                        //Remove the ability to add 
                        break;

                    case Globals.SUPPORTED_TOKENS.CLOSE_BRACKET:
                        if(bracketLevel == 0)
                        {
                            throw new SyntaxErrorException("Close bracket found without supporting open bracket at position - " + i + " .");
                        }
                        bracketLevel--;
                        break;

                    case Globals.SUPPORTED_TOKENS.MULTIPLICATION:
                        //prevent situations like 2+*3 because this doesn't make sense.
                        multAndDivisionCheck(previousToken,i,gatheredTokens.Count);
                        break;

                    case Globals.SUPPORTED_TOKENS.DIVISION:
                        multAndDivisionCheck(previousToken,i,gatheredTokens.Count);
                        break;

                    case Globals.SUPPORTED_TOKENS.ASSIGNMENT:
                        //assignment symbol should be in the 2nd position
                        if(i == 1)
                        {
                            //Only a variable name can be before assignment symbol.
                            if(gatheredTokens[0].GetType() != Globals.SUPPORTED_TOKENS.VARIABLE_NAME)
                            {
                                throw new SyntaxErrorException("Only a variable can be before assignment operator");
                            }

                            //Must have something after the assignment operator
                            if( (i+1) == gatheredTokens.Count)
                            {
                                throw new SyntaxErrorException("Must have an expression that equates to a value after the assignment operator.");
                            }
                        } else
                        {
                            throw new SyntaxErrorException("Assignment operator found in unexpected token position - " + i + ".");
                        }
                        break;

                    case Globals.SUPPORTED_TOKENS.MINUS:
                        if((i+1) == gatheredTokens.Count)
                        {
                            throw new SyntaxErrorException("Integer expected at token position - " + i + ".");
                        }

                        if(gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.DIVISION
                            || gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.INDICIES)
                        {
                            throw new SyntaxErrorException("Integer expected at token position - " + i + ".");
                        }
                        break;

                    case Globals.SUPPORTED_TOKENS.PLUS:
                        if ((i + 1) == gatheredTokens.Count)
                        {
                            throw new SyntaxErrorException("Integer expected at token position - " + (i+1) + ".");
                        }

                        if (gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.DIVISION
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.INDICIES)
                        {
                            throw new SyntaxErrorException("Integer expected at token position - " + (i+1) + ".");
                        }
                        break;

                    case Globals.SUPPORTED_TOKENS.INDICIES:
                        multAndDivisionCheck(previousToken, i,gatheredTokens.Count);
                        break;
                        
                    default:
                        break;
                }

                previousToken = gatheredTokens[i].GetType();
            }

            if(bracketLevel != 0)
            {
                throw new SyntaxErrorException("No supporting closing bracket found.");
            }
        }

        public void multAndDivisionCheck(Globals.SUPPORTED_TOKENS previousToken,int index, int numTokens)
        {
            if (previousToken == Globals.SUPPORTED_TOKENS.MINUS
                            || previousToken == Globals.SUPPORTED_TOKENS.PLUS
                            || previousToken == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || previousToken == Globals.SUPPORTED_TOKENS.DIVISION
                            || previousToken == Globals.SUPPORTED_TOKENS.INDICIES)
            {
                throw new SyntaxErrorException("Integer expected at token position - " + index + ".");
            }

            if(index+1 == numTokens)
            {
                throw new SyntaxErrorException("Integer expected at token position - " + (index + 1) + ".");
            }
        }
    }
}
