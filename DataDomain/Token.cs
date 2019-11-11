using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Token
    {
        private Globals.SUPPORTED_TOKENS type;
        private string value;

        public Token()
        {
            type = Globals.SUPPORTED_TOKENS.WHITE_SPACE;
            value = "";
        }

        public Token(Globals.SUPPORTED_TOKENS type, string value)
        {
            this.type = type;
            this.value = value;
        }

        /*
         * Get the type of token.
         */
        public Globals.SUPPORTED_TOKENS GetType()
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
        public void SetType(Globals.SUPPORTED_TOKENS type)
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
