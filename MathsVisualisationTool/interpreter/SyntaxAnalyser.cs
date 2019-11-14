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
                || gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
            {
                throw new SyntaxErrorException("Expression cannot start with a multiplication, division or a closing bracket.");
            }

            Globals.SUPPORTED_TOKENS previousToken = Globals.SUPPORTED_TOKENS.WHITE_SPACE;

            for(int i=0;i<gatheredTokens.Count;i++)
            {
                switch (gatheredTokens[i].GetType())
                {
                    case Globals.SUPPORTED_TOKENS.OPEN_BRACKET:
                        bracketLevel++;
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
                        multAndDivisionCheck(previousToken,i);
                        break;

                    case Globals.SUPPORTED_TOKENS.DIVISION:
                        multAndDivisionCheck(previousToken,i);
                        break;

                    case Globals.SUPPORTED_TOKENS.VARIABLE_TYPE:
                        //Variable type should be the first token.
                        if(i != 0)
                        {
                            throw new SyntaxErrorException("Variable type found in unexpected position. ");
                        }

                        //Syntax should be VARIABLE_TYPE VARIABLE_NAME = EXPRESSION
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

        public void multAndDivisionCheck(Globals.SUPPORTED_TOKENS previousToken,int index)
        {
            if (previousToken == Globals.SUPPORTED_TOKENS.MINUS
                            || previousToken == Globals.SUPPORTED_TOKENS.PLUS
                            || previousToken == Globals.SUPPORTED_TOKENS.MULTIPLICATION
                            || previousToken == Globals.SUPPORTED_TOKENS.DIVISION)
            {
                throw new SyntaxErrorException("Integer expected at token position - " + index + " .");
            }
        }
    }
}
