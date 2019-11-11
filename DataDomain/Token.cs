using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public enum SUPPORTED_TOKENS
    {
        INTEGER, //supported data types.
        PLUS, MINUS, DIVISION, MULTIPLICATION, //supported ops.
        WHITE_SPACE, OPEN_BRACKET, CLOSE_BRACKET
    }; //Miscellaneous characters.

    public class Token
    {
        private SUPPORTED_TOKENS type;
        private string value;

        public Token()
        {
            type = SUPPORTED_TOKENS.WHITE_SPACE;
            value = "";
        }

        public Token(SUPPORTED_TOKENS type, string value)
        {
            this.type = type;
            this.value = value;
        }

        /*
         * Get the type of token.
         */
        public SUPPORTED_TOKENS GetType()
        {
            return type;
        }

        /*
         * Get the value of the token.
         */
        public string GetValue()
        {
            return value;
        }

        /*
         * Set the token type.
         */
        public void SetType(SUPPORTED_TOKENS type)
        {
            this.type = type;
        }

        /*
         * Set the value of the token.
         */
        public void SetValue(string value)
        {
            this.value = value;
        }

        /*
         * String representation of a Token.
         */
        public override string ToString()
        {
            return "(" + type + "," + value + ")";
        }
    }
}
