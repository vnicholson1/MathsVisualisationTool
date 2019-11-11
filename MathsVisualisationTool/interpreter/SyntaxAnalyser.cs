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

            for(int i=0;i<gatheredTokens.Count;i++)
            {
                /*if(gatheredTokens[0].GetType() == Globals.SUPPORTED_TOKENS.INTEGER)
                {
                    throw new SyntaxErrorException("Integer expected");
                }*/
            }
        }

        /*
         * Things the syntax analyser needs to check: 
         * if there are the same number of open and close brackets.
         * Cannot have consecutive ** or //
         * Number must be followed by an operator.
         * 
         * 
         * 
         */
    }
}
