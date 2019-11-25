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
            Globals.SUPPORTED_TOKENS typeInList = Globals.SUPPORTED_TOKENS.WHITE_SPACE;
            List<char> characters = new List<char>();
            Token tokenToAdd;
            List<Token> tokens = new List<Token>();

            //Go through the line of code added by the user.
            int index = 0;
            foreach( char c in input)
            {
                //Is it a digit?
                if(char.IsDigit(c))
                {
                    //Variable names can contain numbers
                    if(typeInList == Globals.SUPPORTED_TOKENS.VARIABLE_NAME)
                    {
                        characters.Add(c);
                    }
                    else if(typeInList.Equals(Globals.SUPPORTED_TOKENS.CONSTANT))
                    {
                        characters.Add(c);
                    } else
                    {
                        //create a new token and add it to the list.
                        if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                        {
                            tokenToAdd = TokeniseList(characters, typeInList);
                            tokens.Add(tokenToAdd);
                        }
                        characters = new List<char>(){c};
                        typeInList = Globals.SUPPORTED_TOKENS.CONSTANT;
                    }
                }

                else if(c == '+')
                {
                    if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = Globals.SUPPORTED_TOKENS.PLUS;
                }

                else if (c == '-')
                {
                    if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = Globals.SUPPORTED_TOKENS.MINUS;
                }

                else if (c == '*' || c == '\u00D7')
                {
                    if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = Globals.SUPPORTED_TOKENS.MULTIPLICATION;
                }

                else if(c == '/' || c == '\u00F7')
                {
                    if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = Globals.SUPPORTED_TOKENS.DIVISION;
                }

                else if(c == '(')
                {
                    if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = Globals.SUPPORTED_TOKENS.OPEN_BRACKET;
                }

                else if(c == ')')
                {
                    if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = Globals.SUPPORTED_TOKENS.CLOSE_BRACKET;
                }

                else if(c == '=')
                {
                    if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                    {
                        tokenToAdd = TokeniseList(characters, typeInList);
                        tokens.Add(tokenToAdd);
                    }
                    characters = new List<char>() { c };
                    typeInList = Globals.SUPPORTED_TOKENS.ASSIGNMENT;
                }
                else if (char.IsLetter(c))
                {
                    if (typeInList.Equals(Globals.SUPPORTED_TOKENS.VARIABLE_NAME))
                    {
                        characters.Add(c);

                        //Check if there is whitespace after the name
                        if (index + 1 == input.Length || input[(index + 1)] == ' ')
                        {
                            tokenToAdd = TokeniseList(characters, typeInList);
                            tokens.Add(tokenToAdd);

                            characters = new List<char>();
                            typeInList = Globals.SUPPORTED_TOKENS.WHITE_SPACE;
                        }
                    }
                    else
                    {
                        //create a new token and add it to the list.
                        if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
                        {
                            tokenToAdd = TokeniseList(characters, typeInList);
                            tokens.Add(tokenToAdd);
                        }
                        characters = new List<char>() { c };
                        typeInList = Globals.SUPPORTED_TOKENS.VARIABLE_NAME;

                        //if there is whitespace after, then add it to the list of tokens
                        if(index + 1 == input.Length || input[(index+1)] == ' ')
                        {
                            tokenToAdd = TokeniseList(characters, typeInList);
                            tokens.Add(tokenToAdd);

                            characters = new List<char>();
                            typeInList = Globals.SUPPORTED_TOKENS.WHITE_SPACE;
                        }
                    }
                }
                index++;
            }

            if (!typeInList.Equals(Globals.SUPPORTED_TOKENS.WHITE_SPACE))
            {
                tokenToAdd = TokeniseList(characters, typeInList);
                tokens.Add(tokenToAdd);
            }

            PrintTokens(tokens);

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
        private Token TokeniseList(List<char>listOfCharacters, Globals.SUPPORTED_TOKENS listType)
        {
            var value = "";

            foreach(char c in listOfCharacters)
            {
                value += c;
            }

            return new Token(listType,value);
        }
    }
    
}
