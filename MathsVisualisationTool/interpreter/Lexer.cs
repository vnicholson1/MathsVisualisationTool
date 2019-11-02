using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DataDomain;

namespace MathsVisualisationTool
{

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

                if (c == '(')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.OPEN_BRACKET;
                }

                if (c == ')')
                {
                    if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = SUPPORTED_TOKENS.CLOSE_BRACKET;
                }
            }

            if (!typeInList.Equals(SUPPORTED_TOKENS.WHITE_SPACE))
            {
                tokenToAdd = TokeniseList(characters, typeInList);
                tokens.Add(tokenToAdd);
            }

            List<Token> updatedTokens = parenthesiseTokens(tokens);

            PrintTokens(updatedTokens);

            return updatedTokens;
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
        /// Method used to add brackets so that the parser knows what order to do each operation.
        /// For example 3+2*5 will become 3+(2*5) and so on.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Returns a list of tokens with brackets added.</returns>
        private List<Token> parenthesiseTokens(List<Token> tokens)
        {
            List<SUPPORTED_TOKENS> orderOfOperations = new List<SUPPORTED_TOKENS>()  {SUPPORTED_TOKENS.DIVISION,
                                                                                      SUPPORTED_TOKENS.MULTIPLICATION,
                                                                                      SUPPORTED_TOKENS.PLUS,
                                                                                      SUPPORTED_TOKENS.MINUS};

            List<List<Token>> listOfExpressions = new List<List<Token>>();
            //initially all lists in this expression only contain 1 element representing a token.
            foreach(Token t in tokens)
            {
                listOfExpressions.Add(new List<Token>() { t });
            }

            foreach(SUPPORTED_TOKENS s in orderOfOperations)
            {
                int i;
                for(i=0;i<listOfExpressions.Count;i++)
                {
                    if(listOfExpressions[i].Count == 1)
                    {
                        if (listOfExpressions[i][0].GetType() == s)
                        {
                            i = convertListOfExpressions(listOfExpressions, i);
                        }
                    }
                    
                }
            }


            return listOfExpressions[0];
        }

        /// <summary>
        /// Method to Combine expressions together to help with parenthesizing the input.
        /// For example if we had a list of lists = {{1},{+},{2},{*},{3}},
        /// it would become {{1},{+},{(,2,*,3,)}} and this is what this method is doing.
        /// </summary>
        /// <param name="oldList"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int convertListOfExpressions(List<List<Token>> list, int index)
        {
            if(index == 0)
            {
                int right = index + 1;

                //Lists to concatenate
                List<Token> openBracket = new List<Token>() { new Token(SUPPORTED_TOKENS.OPEN_BRACKET, "(") };
                List<Token> op = list[index];
                List<Token> rightSide = list[right];
                List<Token> closeBracket = new List<Token>() { new Token(SUPPORTED_TOKENS.CLOSE_BRACKET, ")") };

                openBracket.AddRange(op);
                openBracket.AddRange(rightSide);
                openBracket.AddRange(closeBracket);

                list.RemoveRange(index, 2);
                list.Insert(index, openBracket);

                return index;

            } else if (index == (list.Count-1))
            {
                int left = index - 1;

                //Lists to concatenate
                List<Token> openBracket = new List<Token>() { new Token(SUPPORTED_TOKENS.OPEN_BRACKET, "(") };
                List<Token> leftSide = list[left];
                List<Token> op = list[index];
                List<Token> closeBracket = new List<Token>() { new Token(SUPPORTED_TOKENS.CLOSE_BRACKET, ")") };

                openBracket.AddRange(leftSide);
                openBracket.AddRange(op);
                openBracket.AddRange(closeBracket);

                list.RemoveRange(left, 2);
                list.Insert(left, openBracket);

                return index-1;
            } else
            {
                int left = index - 1;
                int right = index + 1;

                //Lists to concatenate
                List<Token> openBracket = new List<Token>() { new Token(SUPPORTED_TOKENS.OPEN_BRACKET, "(") };
                List<Token> leftSide = list[left];
                List<Token> rightSide = list[right];
                List<Token> op = list[index];
                List<Token> closeBracket = new List<Token>() { new Token(SUPPORTED_TOKENS.CLOSE_BRACKET, ")") };

                openBracket.AddRange(leftSide);
                openBracket.AddRange(op);
                openBracket.AddRange(rightSide);
                openBracket.AddRange(closeBracket);

                list.RemoveRange(left, 3);
                list.Insert(left, openBracket);
                return index - 1;
            }
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

    
}
