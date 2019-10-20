using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MathsVisualisationTool
{
    public enum GRAMMAR_TOKENS
    {
        EXPRESSION,
        TERM, 
        EXPRESSION1, 
        TERM1,
        PLUS,
        MINUS,
        DIVISION,
        MULTIPLICATION,
        FACTOR,
        INTEGER,
        NULL //For the tree as the root node
    }

    class Parser
    {

        private List<Token> tokens;
        private Token nextToken = null;
        private int index = 0;
        private ParseTree tree;


        public Parser()
        {
            tree = new ParseTree();
        }

        public void AnalyseTokens(List<Token> tokens)
        {
            this.tokens = tokens;
            getNextToken();

            Expression();

            tree.traverseTree();
        }

        //Expression = Term AND Expression1
        public void Expression()
        {
            Term();
            Expression1();
        }

        //Term = Factor AND Term1
        public void Term()
        {
            Factor();
            Term1();
        }

        //Expression1 = PLUS (OR MINUS) AND Term AND EXPRESSION1 OR EMPTY
        public void Expression1()
        {
            bool flag = false;

            if (nextToken.GetType().Equals(SUPPORTED_TOKENS.PLUS))
            {
                //Add plus node
                tree.addChildAndGo(GRAMMAR_TOKENS.PLUS);
                flag = true;
            }
            else if (nextToken.GetType().Equals(SUPPORTED_TOKENS.MINUS))
            {
                //Add minus node
                tree.addChildAndGo(GRAMMAR_TOKENS.MINUS);
                flag = true;
            }

            if(flag)
            {
                getNextToken();
                Term();
                Expression1();
            }
                
            /* empty string */
        }

        //Term1 = MULTIPLICATION (OR DIVISION) AND Factor AND Term1 OR EMPTY
        public void Term1()
        {
            bool flag = false;

            if(nextToken.GetType().Equals(SUPPORTED_TOKENS.MULTIPLICATION))
            {
                //Add multiplication node
                tree.addChildAndGo(GRAMMAR_TOKENS.MULTIPLICATION);
                flag = true;
            } else if (nextToken.GetType().Equals(SUPPORTED_TOKENS.DIVISION))
            {
                //Add division node
                tree.addChildAndGo(GRAMMAR_TOKENS.DIVISION);
                flag = true;
            }

            if(flag)
            {
                getNextToken();
                Factor();
                Term1();
            }
                
            /*empty string */
        }

        //FACTOR = [0-9]
        public void Factor()
        {
            if(nextToken.GetType().Equals(SUPPORTED_TOKENS.INTEGER))
            {
                tree.addChild(GRAMMAR_TOKENS.INTEGER);
               
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
