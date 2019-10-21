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

        /// <summary>
        /// Construct a Parse Tree object with root defined as a null token and a pointer that points 
        /// to the root of the tree.
        /// </summary>
        public ParseTree()
        {
            root = new ParseTreeNode(GRAMMAR_TOKENS.NULL,null);
            pointer = root;
        }

        /// <summary>
        /// Return the root of the tree.
        /// </summary>
        /// <returns></returns>
        public ParseTreeNode getRoot()
        {
            return root;
        }

        /// <summary>
        /// Return the pointer of the tree.
        /// </summary>
        /// <returns></returns>
        public ParseTreeNode getPointer()
        {
            return pointer;
        }

        /// <summary>
        /// Add a left child onto the parse tree and descend onto that node.
        /// </summary>
        /// <param name="newValue"></param>
        public void addLeftChildAndGo(GRAMMAR_TOKENS newValue)
        {
            ParseTreeNode n = new ParseTreeNode(newValue, pointer);

            pointer.getChildren()[0] = n;

            pointer = n;
        }

        /// <summary>
        /// Add a right child onto the parse tree and descend onto that node.
        /// </summary>
        /// <param name="newValue"></param>
        public void addRightChildAndGo(GRAMMAR_TOKENS newValue)
        {
            ParseTreeNode n = new ParseTreeNode(newValue, pointer);

            pointer.getChildren()[1] = n;

            pointer = n;
        }

        /// <summary>
        /// Add a left child onto the parse tree but DO NOT descend onto that node.
        /// </summary>
        /// <param name="newValue"></param>
        public void addLeftChild(GRAMMAR_TOKENS newValue)
        {
            ParseTreeNode n = new ParseTreeNode(newValue, pointer);

            pointer.getChildren()[0] = n;
        }

        /// <summary>
        /// Add a left child onto the parse tree but DO NOT descend onto that node.
        /// </summary>
        /// <param name="newValue"></param>
        public void addRightChild(GRAMMAR_TOKENS newValue)
        {
            ParseTreeNode n = new ParseTreeNode(newValue, pointer);

            pointer.getChildren()[1] = n;
        }

        /// <summary>
        /// Go up to the parent of the node that the pointer object is currently pointing to.
        /// </summary>
        public void goUpOneLevel()
        {
            if(pointer.getParent() != null)
            {
                pointer = pointer.getParent();
            }
        }

        /// <summary>
        /// Implement a Breath First Search of the tree.
        /// Mainly for debugging purposes.
        /// </summary>
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
