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
    class Lexer
    {
        //Supported data types by the lexer.
        private readonly string [] SUPPORTED_DATA_TYPES = new string [] { "INTEGER" };
        //Supported operations by the lexer.
        private readonly string [] SUPPORTED_OPS = new string[] { "PLUS", "MINUS", "DIVISION", "MULTIPLICATION" };
        //Miscellaneous characters.
        private readonly string[] MISC = new string [] { "EOL" };

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

            Token tokenToAdd = new Token();

            //Go through the line of code added by the user.
            foreach( char c in input)
            {
                if(char.IsWhiteSpace(c))
                {
                    //if there where values added 
                    if (!tokenToAdd.isEmpty())
                    {
                        tokens.Add(tokenToAdd);
                        tokenToAdd = new Token();
                    }
                }

                //Is it a digit?
                if(char.IsDigit(c))
                {
                    //if the previous token wasn't an integer
                    if (tokenToAdd.getType() != SUPPORTED_DATA_TYPES[0])
                    {
                        tokens.Add(tokenToAdd);
                    } else if (!tokenToAdd.isEmpty())
                    {
                        //the previous token was an integer before so append it
                        tokenToAdd.appendToValue(c);
                    } else
                    {
                        tokenToAdd = new Token(SUPPORTED_DATA_TYPES[0], char.ToString(c));
                    }
                        
                }

                if(c == '+')
                {
                    //if the previous token wasn't an integer
                    if (tokenToAdd.getType() != SUPPORTED_OPS[0])
                    {
                        tokens.Add(tokenToAdd);
                    }
                    else if (!tokenToAdd.isEmpty())
                    {
                        throw new Exception(" '++' is not a valid expression");
                    }
                    else
                    {
                        tokenToAdd = new Token(SUPPORTED_OPS[0], char.ToString(c));
                    }
                }
            }

            if(!tokenToAdd.isEmpty())
            {
                tokens.Add(tokenToAdd);
            }

            foreach(Token t in tokens)
            {
                Console.WriteLine(t.ToString());
            }

            Console.WriteLine("--------------------------------------------------");
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
        private string type;
        private string value;

        public Token()
        {
            this.type = "";
            this.value = "";
        }

        public Token(string type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public void appendToValue(char valueToAppend)
        {
            this.value += valueToAppend;
        }

        public string getType()
        {
            return type;
        }

        public string getValue()
        {
            return value;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public void setValue(string value)
        {
            this.value = value;
        }

        public bool isEmpty()
        {
            return (value.Length == 0); 
        }

        public override string ToString()
        {
            return "(" + this.type + "," + this.value + ")";
        }
    }
}
