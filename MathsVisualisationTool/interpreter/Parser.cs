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

            //Add the expression node onto the tree.
            tree.addLeftChild(GRAMMAR_TOKENS.EXPRESSION);
            tree.goDownLeft();
            Expression();

            tree.traverseTree();
        }

        //Expression = Term AND Expression1
        public void Expression()
        {
            //Add the term node onto the tree.
            tree.addLeftChild(GRAMMAR_TOKENS.TERM);
            tree.goDownLeft();
            Term();

            //Add the expression1 node onto the tree
            tree.addRightChild(GRAMMAR_TOKENS.EXPRESSION1);
            tree.goDownRight();
            Expression1();

            tree.goUpOneLevel();
        }

        //Term = Factor AND Term1
        public void Term()
        {
            //Add the factor node onto the tree.
            tree.addLeftChild(GRAMMAR_TOKENS.FACTOR);
            tree.goDownLeft();
            Factor();

            //Add the term1 node
            tree.addRightChild(GRAMMAR_TOKENS.TERM1);
            tree.goDownRight();
            Term1();

            tree.goUpOneLevel();
        }

        //Expression1 = PLUS (OR MINUS) AND Term AND EXPRESSION1 OR EMPTY
        public void Expression1()
        {
            bool flag = false;

            if (nextToken.GetType().Equals(SUPPORTED_TOKENS.PLUS))
            {
                //Add plus node
                tree.addLeftChild(GRAMMAR_TOKENS.PLUS);
                tree.goDownLeft();

                flag = true;
            }
            else if (nextToken.GetType().Equals(SUPPORTED_TOKENS.MINUS))
            {
                //Add minus node
                tree.addLeftChild(GRAMMAR_TOKENS.MINUS);
                tree.goDownLeft();

                flag = true;
            }

            if(flag)
            {
                getNextToken();

                //Add a new term node
                tree.addLeftChild(GRAMMAR_TOKENS.TERM);
                tree.goDownLeft();
                Term();

                //Add a new expression 1 node
                tree.addRightChild(GRAMMAR_TOKENS.EXPRESSION1);
                tree.goDownRight();
                Expression1();
            }
                
            /* empty string */

            tree.goUpOneLevel();
        }

        //Term1 = MULTIPLICATION (OR DIVISION) AND Factor AND Term1 OR EMPTY
        public void Term1()
        {
            bool flag = false;

            if(nextToken.GetType().Equals(SUPPORTED_TOKENS.MULTIPLICATION))
            {
                //Add multiplication node
                tree.addLeftChild(GRAMMAR_TOKENS.MULTIPLICATION);
                tree.goDownLeft();

                flag = true;
            } else if (nextToken.GetType().Equals(SUPPORTED_TOKENS.DIVISION))
            {
                //Add division node
                tree.addLeftChild(GRAMMAR_TOKENS.DIVISION);
                tree.goDownLeft();

                flag = true;
            }

            if(flag)
            {
                getNextToken();

                //Add a new factor node
                tree.addLeftChild(GRAMMAR_TOKENS.FACTOR);
                tree.goDownLeft();
                Factor();

                //Add a new term 1 node
                tree.addRightChild(GRAMMAR_TOKENS.TERM1);
                tree.goDownRight();
                Term1();
            }
                
            /*empty string */

            tree.goUpOneLevel();
        }

        //FACTOR = [0-9]
        public void Factor()
        {
            if(nextToken.GetType().Equals(SUPPORTED_TOKENS.INTEGER))
            {
                tree.addLeftChild(GRAMMAR_TOKENS.INTEGER);
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
