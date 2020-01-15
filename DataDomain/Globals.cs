using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public static class Globals
    {
        public enum SUPPORTED_TOKENS
        {
            CONSTANT,PI,EULER, //supported data types
            PLUS, MINUS, DIVISION, MULTIPLICATION, ASSIGNMENT, INDICIES, //supported ops.
            VARIABLE_NAME, //tokens related to the assignment of variables
            WHITE_SPACE, OPEN_BRACKET, CLOSE_BRACKET, COMMA, LESS_THAN, GREATER_THAN, //miscellaneous characters.
            PLOT,SIN,COS,TAN,LOG,LN,SQRT,ROOT,ABS,FIBONACCI //supported functions
        };

        //Config variables to determine whether to draw the graph onto the canvas, live charts or both.
        public static bool SHOW_GRAPH_CANVAS = true;
        public static bool SHOW_LIVE_CHARTS = true;

        //record the string rep of the keywords.
        public static List<string> keyWords = new List<string>() { "plot","sin","cos","tan","log","ln","sqrt","root","abs","fib"};
        //record the functions that have more than one argument.
        public static List<string> funcsWith2Args = new List<string>() { "log","root" };
        //record the order of each operation.
        public static List<SUPPORTED_TOKENS> orderOfOperators = new List<SUPPORTED_TOKENS>()
        {SUPPORTED_TOKENS.INDICIES,SUPPORTED_TOKENS.DIVISION, SUPPORTED_TOKENS.MULTIPLICATION, SUPPORTED_TOKENS.PLUS,SUPPORTED_TOKENS.MINUS};

        /// <summary>
        /// Function to get the token type based on a given word.
        /// Must be a keyword otherwise an ArgumentException is thrown.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static SUPPORTED_TOKENS getTokenFromKeyword(string word)
        {
            if(word == "plot")
            {
                return SUPPORTED_TOKENS.PLOT;
            } else if (word == "sin")
            {
                return SUPPORTED_TOKENS.SIN;
            } else if (word == "cos")
            {
                return SUPPORTED_TOKENS.COS;
            } else if (word == "tan")
            {
                return SUPPORTED_TOKENS.TAN;
            } else if (word == "log")
            {
                return SUPPORTED_TOKENS.LOG;
            }
            else if (word == "ln")
            {
                return SUPPORTED_TOKENS.LN;
            }
            else if (word == "sqrt")
            {
                return SUPPORTED_TOKENS.SQRT;
            }
            else if (word == "root")
            {
                return SUPPORTED_TOKENS.ROOT;
            }
            else if (word == "abs")
            {
                return SUPPORTED_TOKENS.ABS;
            } 
            else if (word == "fib")
            {
                return SUPPORTED_TOKENS.FIBONACCI;
            }
            else
            {
                //Normally shouldn't happen but if someone else makes a mistake then this
                //should hopefully be clear enough.
                throw new ArgumentException("Word given is not a keyword.");
            }
        }

    }
}
