using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MathsVisualisationTool
{ 

    public enum GRAMMAR_TOKENS { NULL};
    class Parser
    {

        private List<Token> tokens;
        private Token nextToken = null;
        private int index = 0;
        //private ParseTree tree;
        

        public Parser()
        {
            //tree = new ParseTree();
        }

        public void AnalyseTokens(List<Token> tokens)
        {
            this.tokens = tokens;
            preProcessTokens();
        }

        /// <summary>
        /// Method to perform certain preprocessing tasks on the tokens. 
        /// 1) Check for consecutive operations.
        /// 2) Add parenthesis onto the expression so that it follows bidmas e.g. 2+3*2 will
        /// evaluate to 2+(3*2).
        /// </summary>
        /// <param name="tokens"></param>
        public void preProcessTokens()
        {
            PreProcessor p = new PreProcessor(tokens);
            p.checkForConsecutiveOps();
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

    /// <summary>
    /// Class specifically for the pre-processing of expressions.
    /// </summary>
    class PreProcessor
    {

        private List<Token> gatheredTokens;

        public PreProcessor(List<Token> tokens)
        {
            gatheredTokens = tokens;
        }

        /// <summary>
        /// Function to check if there are consecutive ops present e.g. '++' is not allowed.
        /// </summary>
        public void checkForConsecutiveOps()
        {
            int numOpTokens = 0;
            int count = 0;
            List<SUPPORTED_TOKENS> opTokens = new List<SUPPORTED_TOKENS>() {SUPPORTED_TOKENS.DIVISION,
                                                                            SUPPORTED_TOKENS.MINUS,
                                                                            SUPPORTED_TOKENS.MULTIPLICATION,
                                                                            SUPPORTED_TOKENS.PLUS};
            foreach (Token t in gatheredTokens)
            {
                if (opTokens.Contains(t.GetType()))
                {
                    if (numOpTokens != 0)
                    {
                        throw new Exception("Integer expected at token position - " + count);
                    }
                    else
                    {
                        numOpTokens++;
                    }
                }
                else
                {
                    numOpTokens = 0;
                }
                count++;
            }
        }

        /// <summary>
        /// Function to add parenthesis to ensure the order of operations is correct.
        /// </summary>
        public List<Token> addParentheses()
        {
            //Firstly check for division


            foreach(Token t in gatheredTokens)
            {
                Console.WriteLine(t.ToString());
            }
            return new List<Token>();
        }
    }
}
