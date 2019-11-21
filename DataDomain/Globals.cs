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
            CONSTANT, //supported data types
            PLUS, MINUS, DIVISION, MULTIPLICATION, ASSIGNMENT, INDICIES, //supported ops.
            VARIABLE_NAME, //tokens related to the assignment of variables
            WHITE_SPACE, OPEN_BRACKET, CLOSE_BRACKET, //miscellaneous characters.
            PLOT //supported functions
        };

        //record the string rep of the keywords.
        public static List<string> keyWords = new List<string>() { "plot" };
        //record the SUPPORTED_TOKENS rep of the keywords.
        public static List<SUPPORTED_TOKENS> keyWordTokens = new List<SUPPORTED_TOKENS>() { SUPPORTED_TOKENS.PLOT };
        
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
            } else
            {
                //Normally shouldn't happen but if someone else makes a mistake then this
                //should haopefully be clear enough.
                throw new ArgumentException("Word given is not a keyword.");
            }
        }
    }
}
