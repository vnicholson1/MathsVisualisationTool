using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MathsVisualisationTool
{
    class Parser
    {

        private List<Token> tokens;
        private Token nextToken = null;
        private int index = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void AnalyseTokens()
        {
            getNextToken();
            Expression();
        }

        public void Expression()
        {
            //Expression = Term AND Expression1
            Term();
            Expression1();
        }

        public void Term()
        {
            //Term = Factor AND Term1
            Factor();
            Term1();
        }

        public void Expression1()
        {
            //Expression1 = PLUS (OR MINUS) AND Term AND EXPRESSION1 OR EMPTY
            if (nextToken.GetType().Equals(SUPPORTED_TOKENS.PLUS) || nextToken.GetType().Equals(SUPPORTED_TOKENS.MINUS))
            {
                getNextToken();
                Term();
                Expression1();
            } else {
                /* empty string */
            }
        }

        public void Term1()
        {
            //Term1 = MULTIPLICATION (OR DIVISION) AND Factor AND Term1 OR EMPTY
            if(nextToken.GetType().Equals(SUPPORTED_TOKENS.MULTIPLICATION) || nextToken.GetType().Equals(SUPPORTED_TOKENS.DIVISION))
            {
                getNextToken();
                Factor();
                Term1();
            } else
            {
                /*empty string */
                /*because it doesn't have to have the multiplication symbol */
            }
        }

        public void Factor()
        {
            //FACTOR = [0-9]
            if(nextToken.GetType().Equals(SUPPORTED_TOKENS.INTEGER))
            {
                getNextToken();
            } else
            {
                error("INTEGER EXPECTED at token position - " + (index-1));
            }
        }

        private void error(string msg)
        {
            throw new Exception(msg);
        }

        private void getNextToken()
        {
            if(index < tokens.Count)
            {
                nextToken = tokens[index];
                index++;
            } else
            {
                nextToken = tokens[(tokens.Count - 1)];
            }
        }
    }
}
