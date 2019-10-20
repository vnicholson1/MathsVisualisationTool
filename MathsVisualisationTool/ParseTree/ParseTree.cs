using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{

    class ParseTree
    {

        private ParseTreeNode root;
        //current is just going to be a pointer to what ever current node the tree is looking at.
        private ParseTreeNode pointer;

        public ParseTree()
        {
            root = new ParseTreeNode(GRAMMAR_TOKENS.NULL,null);
            pointer = root;
        }

        public ParseTreeNode getRoot()
        {
            return root;
        }

        public ParseTreeNode getPointer()
        {
            return pointer;
        }

        /// <summary>
        /// Add a left child onto the node the pointer is pointing to.
        /// </summary>
        /// <param name="newNode"></param>
        public void addLeftChild(GRAMMAR_TOKENS newValue)
        {
            ParseTreeNode n = new ParseTreeNode(newValue, pointer);
            pointer.getChildren()[0] = n;
        }

        /// <summary>
        /// Add a left child onto the node the pointer is pointing to.
        /// </summary>
        /// <param name="newNode"></param>
        public void addRightChild(GRAMMAR_TOKENS newValue)
        {
            ParseTreeNode n = new ParseTreeNode(newValue, pointer);
            pointer.getChildren()[1] = n;
        }

        public void goUpOneLevel()
        {
            pointer = pointer.getParent();
        }

        public void goDownLeft()
        {
            pointer = pointer.getChildren()[0];
        }

        public void goDownRight()
        {
            pointer = pointer.getChildren()[1];
        }

        public void traverseTree()
        {
            Queue<ParseTreeNode> nodes_to_visit = new Queue<ParseTreeNode>();
            nodes_to_visit.Enqueue(root);

            while(nodes_to_visit.Count != 0)
            {
                ParseTreeNode current = nodes_to_visit.Dequeue();
                if(current is null)
                {
                    Console.WriteLine("NULL");
                } else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        ParseTreeNode child = current.getChildren()[i];
                        nodes_to_visit.Enqueue(child);
                    }
                    Console.WriteLine(current.getValue());
                }
                
            }
        }

    }
}
