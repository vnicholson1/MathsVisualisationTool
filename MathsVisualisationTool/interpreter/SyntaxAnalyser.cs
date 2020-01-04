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

            if(gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION) {
                throw new IncorrectMultiplicationOperatorPositionException("Expression cannot start with a multiplication operator");
            } else if(gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.DIVISION)
            {
                throw new IncorrectDivisionOperatorPositionException("Expression cannot start with a division operator");
            } else if(gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
            {
                throw new IncorrectIndiciesOperatorPositionException("Expression cannot start with a close bracket");
            } else if(gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.INDICIES)
            {
                throw new IncorrectCloseBracketPositionException("Expression cannot start with a indicies operator");
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
                                throw new VariableAfterConstantException("Cannot have variable name straight after constant. Did you mean " +
                                    gatheredTokens[i].GetValue() + "*" + gatheredTokens[(i + 1)].GetValue() + "?");
                            }
                                //for situations like 2pi - you want the user to put 2*pi.
                            if (gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.CONSTANT)
                            {
                                throw new ConstantAfterConstantException("Cannot have a constant straight after constant. Did you mean " +
                                    gatheredTokens[i].GetValue() + "*" + gatheredTokens[(i + 1)].GetValue() + "?");
                            }
                        }

                        //Check that only one decimal place has been added or no "." cases
                        int count = gatheredTokens[i].GetValue().Count(x => x == '.');

                        if(count > 1)
                        {
                            throw new InvalidNumberException("Cannot have more than one decimal point in number.");
                        }
                        //if the number specified is just the decimal point.
                        if (gatheredTokens[i].GetValue() == ".")
                        {
                            throw new InvalidNumberException("Unrecognised number - '.'.");
                        }

                        break;

                    case Globals.SUPPORTED_TOKENS.OPEN_BRACKET:
                        bracketLevel++;
                        if(i+1 == gatheredTokens.Count)
                        {
                            throw new IncorrectOpenBracketPositionException("Expression cannot finish with an open bracket.");
                        }

                        //Remove the ability to add ()
                        if(gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
                        {
                            throw new MissingExpressionBetweenBracketsException("Enclosing brackets must contain an expression.");
                        }

                        break;

                    case Globals.SUPPORTED_TOKENS.CLOSE_BRACKET:
                        if(previousToken == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || previousToken == Globals.SUPPORTED_TOKENS.DIVISION
                            || previousToken == Globals.SUPPORTED_TOKENS.INDICIES)
                        {
                            throw new IncorrectOpenBracketPositionException("Integer expected at token position - " + i + " .");
                        }

                        if(bracketLevel == 0)
                        {
                            throw new MissingOpenBracketException("Close bracket found without supporting open bracket at position - " + i + " .");
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
                                throw new InvalidTypeBeforeAssignmentOperatorException("Only a variable can be before assignment operator");
                            }

                            //Must have something after the assignment operator
                            if( (i+1) == gatheredTokens.Count)
                            {
                                throw new MissingExpressionAfterOperatorException("Must have an expression that equates to a value after the assignment operator.");
                            }
                        } else
                        {
                            throw new IncorrectAssignmentOperatorPositionException("Assignment operator found in unexpected token position - " + i + ".");
                        }

                        if(gatheredTokens[(i+1)].GetType() != Globals.SUPPORTED_TOKENS.PLUS
                            && gatheredTokens[(i + 1)].GetType() != Globals.SUPPORTED_TOKENS.MINUS
                            && gatheredTokens[(i + 1)].GetType() != Globals.SUPPORTED_TOKENS.CONSTANT
                            && gatheredTokens[(i + 1)].GetType() != Globals.SUPPORTED_TOKENS.VARIABLE_NAME
                            && gatheredTokens[(i + 1)].GetType() != Globals.SUPPORTED_TOKENS.OPEN_BRACKET)
                        {
                            throw new InvalidTypeAfterAssignmentOperatorException("Invalid token " + gatheredTokens[(i+1)].GetValue()  + " found after assignment operator.");
                        }
                        break;

                    case Globals.SUPPORTED_TOKENS.MINUS:
                        if((i+1) == gatheredTokens.Count)
                        {
                            throw new MissingExpressionAfterOperatorException("Integer expected at token position - " + i + ".");
                        }

                        if(gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.DIVISION
                            || gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || gatheredTokens[(i+1)].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.INDICIES)
                        {
                            throw new InvalidTypeAfterMinusOperatorException("Integer expected at token position - " + i + ".");
                        }
                        break;

                    case Globals.SUPPORTED_TOKENS.PLUS:
                        if ((i + 1) == gatheredTokens.Count)
                        {
                            throw new MissingExpressionAfterOperatorException("Integer expected at token position - " + (i+1) + ".");
                        }

                        if (gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.DIVISION
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET
                            || gatheredTokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.INDICIES)
                        {
                            throw new InvalidTypeAfterPlusOperatorException("Integer expected at token position - " + (i+1) + ".");
                        }
                        break;

                    case Globals.SUPPORTED_TOKENS.INDICIES:
                        multAndDivisionCheck(previousToken, i,gatheredTokens.Count);
                        break;

                    case Globals.SUPPORTED_TOKENS.LESS_THAN:
                        throw new IncorrectLessThanOperatorPositionException("< symbol found in unexpected position.");

                    case Globals.SUPPORTED_TOKENS.GREATER_THAN:
                        throw new IncorrectGreaterThanOperatorPositionException("> symbol found in unexpected position.");

                    default:
                        break;
                }

                previousToken = gatheredTokens[i].GetType();
            }

            if(bracketLevel != 0)
            {
                throw new MissingCloseBracketException("No supporting closing bracket found.");
            }
        }

        public void multAndDivisionCheck(Globals.SUPPORTED_TOKENS previousToken,int index, int numTokens)
        {
            if (previousToken == Globals.SUPPORTED_TOKENS.MINUS
                            || previousToken == Globals.SUPPORTED_TOKENS.PLUS
                            || previousToken == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || previousToken == Globals.SUPPORTED_TOKENS.DIVISION
                            || previousToken == Globals.SUPPORTED_TOKENS.INDICIES
                            || previousToken == Globals.SUPPORTED_TOKENS.OPEN_BRACKET)
            {
                if(gatheredTokens[index].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION)
                {
                    throw new InvalidTypeBeforeMultiplicationOperatorException("Integer expected at token position - " + index + ".");
                } else if(gatheredTokens[index].GetType() == Globals.SUPPORTED_TOKENS.DIVISION)
                {
                    throw new InvalidTypeBeforeDivisionOperatorException("Integer expected at token position - " + index + ".");
                } else if(gatheredTokens[index].GetType() == Globals.SUPPORTED_TOKENS.INDICIES)
                {
                    throw new InvalidTypeBeforeIndiciesOperatorException("Integer expected at token position - " + index + ".");
                }
                
            }

            if(index+1 == numTokens)
            {
                throw new MissingExpressionAfterOperatorException("Integer expected at token position - " + (index + 1) + ".");
            }
        }
    }
}
