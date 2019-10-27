using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MathsVisualisationTool
{
    
    public enum SUPPORTED_TOKENS { INTEGER, //supported data types.
                                   PLUS, MINUS, DIVISION, MULTIPLICATION, //supported ops.
                                   WHITE_SPACE}; //Miscellaneous characters mainly used by the parser and the lexer.

    class Lexer
    {
        
        public Lexer()
        {

        }

        /*
         * Function to tokenise the user's input. 
         */
        public List<Token> TokeniseInput(string input)
        {
            SUPPORTED_TOKENS typeInList = SUPPORTED_TOKENS.WHITE_SPACE;
            List<char> characters = new List<char>();
            Token tokenToAdd;
            List<Token> tokens = new List<Token>();

            //Go through the line of code added by the user.
            foreach( char c in input)
            {
                if(char.IsWhiteSpace(c))
                {
                    continue;
                }

                //Is it a digit?
                if(char.IsDigit(c))
                {
                    
                    if(typeInList.Equals(SUPPORTED_TOKENS.INTEGER))
                    {
                        characters.Add(c);
                    } else
                    {
                        
                        //create a new token and add it to the list.
                        if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                        {
                            tokenToAdd = TokeniseList(characters, typeInList);
                            tokens.Add(tokenToAdd);
                        }
                        characters = new List<char>(){c};
                        typeInList = SUPPORTED_TOKENS.INTEGER;
                    }
                }

                if(c == '+')
                {
                    //For now if there are consecutive '+' ops it labels them as two seperate + symbols.
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.PLUS;
                }

                if (c == '-')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.MINUS;
                }

                if (c == '*')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.MULTIPLICATION;
                }

                if (c == '/')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.DIVISION;
                }
            }

            if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
            {
                tokenToAdd = TokeniseList(characters, typeInList);
                tokens.Add(tokenToAdd);
            }

            //PrintTokens(tokens);

            return tokens;
        }

        /// <summary>
        /// Method to print out the list of collected tokens. Mainly for debugging purposes.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private void PrintTokens(List<Token> tokens)
        {
            foreach (Token t in tokens)
            {
                Console.WriteLine(t.ToString());
            }

            Console.WriteLine("--------------------------------------------------");
        }

        /// <summary>
        /// Convert the array of characters into a list of tokens.
        /// </summary>
        /// <param name="listOfCharacters"></param>
        /// <param name="listType"></param>
        /// <returns></returns>
        private Token TokeniseList(List<char>listOfCharacters, SUPPORTED_TOKENS listType)
        {
            string value = "";

            foreach(char c in listOfCharacters)
            {
                value += c;
            }

            return new Token(listType,value);
        }

        /// <summary>
        /// Function to load the interpreter's configuration file stored in config.
        /// </summary>
        /// <returns></returns>
        private JToken LoadInterpreterConfig()
        {
            //Get current WORKING directory (i.e. \bin\debug)
            string workingDirectory = Directory.GetCurrentDirectory();

            //Get PROJECT directory 
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            string filePath = Path.GetFullPath(Path.Combine(projectDirectory + "\\config\\InterpreterConfig.json"));
            JObject jsonObject = JObject.Parse(File.ReadAllText(filePath));

            JToken con = jsonObject["INTERPRETER_CONFIG"];

            return con; 
        }
    }

    class Token
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
