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
            CONSTANT_INT, //supported data types
            PLUS, MINUS, DIVISION, MULTIPLICATION, ASSIGNMENT, //supported ops.
            VARIABLE_TYPE,VARIABLE_NAME, //tokens related to the assignment of variables
            WHITE_SPACE, OPEN_BRACKET, CLOSE_BRACKET //Miscellaneous characters.
        };

        public static List<string> reservedWords = new List<string>() { "float", "int" }; //For defining variable types.
        
        public static List<SUPPORTED_TOKENS> orderOfOperators = new List<SUPPORTED_TOKENS>()
        {SUPPORTED_TOKENS.DIVISION, SUPPORTED_TOKENS.MULTIPLICATION, SUPPORTED_TOKENS.PLUS,SUPPORTED_TOKENS.MINUS};
    }
}
