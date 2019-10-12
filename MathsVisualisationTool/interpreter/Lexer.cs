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
                                   EOL, WHITE_SPACE}; //Miscellaneous characters.

    class Lexer
    {

        private List<Token> tokens;
        
        public Lexer()
        {
            tokens = new List<Token>();
        }

        /*
         * Function to tokenise the user's input. 
         */
        public void tokeniseInput(string input)
        {
            SUPPORTED_TOKENS typeInList = SUPPORTED_TOKENS.WHITE_SPACE;
            List<char> characters = new List<char>();
            Token tokenToAdd;

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
                            tokenToAdd = tokeniseList(characters, typeInList);
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
                        tokenToAdd = tokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.PLUS;
                }

                if (c == '-')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = tokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.MINUS;
                }

                if (c == '*')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = tokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.MULTIPLICATION;
                }

                if (c == '/')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = tokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.DIVISION;
                }
            }

            if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
            {
                tokenToAdd = tokeniseList(characters, typeInList);
                tokens.Add(tokenToAdd);
            }

            foreach (Token t in tokens)
            {
                Console.WriteLine(t.ToString());
            }

            Console.WriteLine("--------------------------------------------------");
        }

        private Token tokeniseList(List<char>listOfCharacters, SUPPORTED_TOKENS listType)
        {
            string value = "";

            foreach(char c in listOfCharacters)
            {
                value += c;
            }

            return new Token(listType,value);
        }

        /*
         * Function to load the interpreter's configuration file stored in config.
         */
        private JToken loadInterpreterConfig()
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

        /**
         * Method to add a character onto the existing token
         */
        public void appendToValue(char valueToAppend)
        {
            value += valueToAppend;
        }

        /*
         * Get the type of token.
         */
        public SUPPORTED_TOKENS getType()
        {
            return type;
        }

        /*
         * Get the value of the token.
         */
        public string getValue()
        {
            return value;
        }

        /*
         * Set the token type.
         */
        public void setType(SUPPORTED_TOKENS type)
        {
            this.type = type;
        }

        /*
         * Set the value of the token.
         */
        public void setValue(string value)
        {
            this.value = value;
        }

        /// <summary>
        /// Method to set the field values of this token class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void setFieldValues(SUPPORTED_TOKENS type, string value)
        {
            this.type = type;
            this.value = value;
        }

        /*
         * Test if this token's value is empty.
         */
        public bool isEmpty()
        {
            return (value.Length == 0); 
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
