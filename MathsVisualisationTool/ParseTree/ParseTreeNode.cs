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

        public ParseTreeNode(GRAMMAR_TOKENS value,ParseTreeNode parent)
        {
            this.value = value;
            children = new ParseTreeNode [2];
            this.parent = parent; 
        }

        public ParseTreeNode [] getChildren()
        {
            return children;
        }

        public GRAMMAR_TOKENS getValue()
        {
            return value;
        }

        public ParseTreeNode getParent()
        {
            return parent;
        }

        public void setValue(GRAMMAR_TOKENS value)
        {
            this.value = value;
        }

        public void setParent(ParseTreeNode parent)
        {
            this.parent = parent;
        }
    }
}

