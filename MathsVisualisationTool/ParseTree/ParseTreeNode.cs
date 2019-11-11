using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{

    class ParseTreeNode
    {

        private ParseTreeNode [] children;
        private List<Token> value;
        ParseTreeNode parent;

        /// <summary>
        /// Create a parse tree node object with node value the grammar token and a pointer to its parent. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        public ParseTreeNode(List<Token> value,ParseTreeNode parent)
        {
            this.value = value;
            children = new ParseTreeNode [2];
            this.parent = parent; 
        }

        /// <summary>
        /// Get the children of this current node.
        /// </summary>
        /// <returns></returns>
        public ParseTreeNode [] getChildren()
        {
            return children;
        }

        /// <summary>
        /// Get the value stored in this node object.
        /// </summary>
        /// <returns></returns>
        public List<Token> getValue()
        {
            return value;
        }

        /// <summary>
        /// Get the parent of this current node.
        /// </summary>
        /// <returns></returns>
        public ParseTreeNode getParent()
        {
            return parent;
        }

        /// <summary>
        /// Set the value of the node.
        /// </summary>
        /// <param name="value"></param>
        public void setValue(List<Token> value)
        {
            this.value = value;
        }

        /// <summary>
        /// Set the parent of the node.
        /// </summary>
        /// <param name="parent"></param>
        public void setParent(ParseTreeNode parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Method for testing whether this node is a leaf node.
        /// </summary>
        /// <returns></returns>
        public bool isLeafNode()
        {
            return ((children[0] == null) && (children[1] == null));
        }

        /// <summary>
        /// Splits the node into two child nodes by the first occurance of the given operation and sets this node's
        /// value as the given operation 
        /// e.g. 2
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>True if the operation was split successfully, false otherwise (no operation was found).</returns>
        public bool splitNodeByOp(Globals.SUPPORTED_TOKENS operation)
        {
            int index = 0;
            foreach(Token t in value)
            {
                if(t.Equals(operation))
                {
                    //get the left hand side of the expression.
                    List<Token> leftTokens = new List<Token>();
                    for(int i=0;i<index;i++)
                    {
                        leftTokens.Add(value[i]);
                    }
                    //get the right hand side of the expression.
                    List<Token> rightTokens = new List<Token>();
                    for(int i=(index+1);i<value.Count;i++)
                    {
                        rightTokens.Add(value[i]);
                    }

                    children[0] = new ParseTreeNode(leftTokens, this);
                    children[1] = new ParseTreeNode(rightTokens, this);
                    value = new List<Token>() { new Token(operation, "") };
                    return true;
                }
                index++;
            }
            return false;
        }
    }
}

