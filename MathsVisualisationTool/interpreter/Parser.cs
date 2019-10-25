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
        private ParseTree tree;
        

        public Parser()
        {
            tree = new ParseTree();
        }

        public void AnalyseTokens(List<Token> tokens)
        {
            this.tokens = tokens;

            List<SUPPORTED_TOKENS> orderOfOperations = new List<SUPPORTED_TOKENS>() { SUPPORTED_TOKENS.DIVISION,
                                                                                      SUPPORTED_TOKENS.MULTIPLICATION,
                                                                                      SUPPORTED_TOKENS.PLUS,
                                                                                      SUPPORTED_TOKENS.MINUS};

            //set the root node as the entire expression.
            tree.getRoot().setValue(tokens);
            List<ParseTreeNode> leafNodes = tree.getLeafNodes();

            foreach(SUPPORTED_TOKENS s in orderOfOperations)
            {
                //while all leaf nodes are not just integers.
                //while(leafNodes)
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
