using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{

    class ParseTreeNode
    {

        private ParseTreeNode [] children;
        private GRAMMAR_TOKENS value;
        ParseTreeNode parent;

        /// <summary>
        /// Create a parse tree node object with node value the grammar token and a pointer to its parent. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parent"></param>
        public ParseTreeNode(GRAMMAR_TOKENS value,ParseTreeNode parent)
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
        public GRAMMAR_TOKENS getValue()
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
        public void setValue(GRAMMAR_TOKENS value)
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
    }
}

