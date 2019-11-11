using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    class Preprocessor
    {

        private List<Token> gatheredTokens;

        /// <summary>
        /// Class to preprocess the given tokens. It deals with the order that the parser
        /// should do each operation (i.e. BIDMAS) and situations where you have consecutive ops 
        /// e.g. 3++3 should just evaluate to 3+3.
        /// </summary>
        /// <param name="tokens"></param>
        public Preprocessor(List<Token> tokens)
        {
            gatheredTokens = tokens;
        }

        public List<Token> processTokens()
        {
            combinePlusAndMinusOps(gatheredTokens);

            createNegativeAndPositiveNumbers(gatheredTokens);

            List<Token> updatedTokens = parenthesiseTokens(gatheredTokens);

            return updatedTokens;
        }

        /// <summary>
        /// Method used to add brackets so that the parser knows what order to do each operation.
        /// For example 3+2*5 will become 3+(2*5) and so on.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns>Returns a list of tokens with brackets added.</returns>
        private List<Token> parenthesiseTokens(List<Token> tokens)
        {

            List<List<Token>> listOfExpressions = new List<List<Token>>();

            //initially all lists in this expression only contain 1 element representing a token unless it is enclosed in brackets.
            for (int i = 0; i < tokens.Count; i++)
            {
                //Record the end index of the expression enclosed in brackets
                int endOfExp = -1;
                if (tokens[i].GetType() == Globals.SUPPORTED_TOKENS.OPEN_BRACKET)
                {
                    int bracketLevel = -1;
                    for (int j = i; j < tokens.Count; j++)
                    {
                        if (tokens[j].GetType() == Globals.SUPPORTED_TOKENS.OPEN_BRACKET)
                        {
                            bracketLevel++;
                        }
                        else if (tokens[j].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
                        {
                            if (bracketLevel == 0)
                            {
                                endOfExp = j;
                                break;
                            }
                            else
                            {
                                bracketLevel--;
                            }
                        }
                    }
                    if (endOfExp == -1)
                    {
                        endOfExp = tokens.Count - 1;
                    }
                    //Collect the expression enclosed inside the bracket
                    List<Token> gatheredExpression = tokens.GetRange(i + 1, endOfExp - i - 1);
                    //parenthesise it
                    List<Token> parenthesisedExpression = parenthesiseTokens(gatheredExpression);
                    //then add it to the list of expressions
                    listOfExpressions.Add(parenthesisedExpression);
                    i += gatheredExpression.Count + 1;
                }
                else
                {
                    listOfExpressions.Add(new List<Token>() { tokens[i] });
                }
            }

            foreach (Globals.SUPPORTED_TOKENS s in Globals.orderOfOperators)
            {
                int i;
                for (i = 0; i < listOfExpressions.Count; i++)
                {
                    if (listOfExpressions[i].Count == 1)
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
        /// Method for creating a token that is negative or positive given certain conditions.
        /// i.e. Nothing - int -> create negative token
        /// * - int -> create a negative token.
        /// / - int -> create a negative token.
        /// </summary>
        /// <param name="tokens"></param>
        private void createNegativeAndPositiveNumbers(List<Token> tokens)
        {
            //For situations like +3 or -3 as an input
            switch (tokens[0].GetType())
            {
                case Globals.SUPPORTED_TOKENS.PLUS:
                    if (tokens[1].GetType() == Globals.SUPPORTED_TOKENS.INTEGER)
                    {
                        string tokenValue = tokens[1].GetValue();
                        tokens.RemoveRange(0, 2);
                        tokens.Insert(0, new Token(Globals.SUPPORTED_TOKENS.INTEGER, tokenValue));
                    }
                    break;
                case Globals.SUPPORTED_TOKENS.MINUS:
                    if (tokens[1].GetType() == Globals.SUPPORTED_TOKENS.INTEGER)
                    {
                        string tokenValue = tokens[1].GetValue();
                        tokens.RemoveRange(0, 2);
                        tokens.Insert(0, new Token(Globals.SUPPORTED_TOKENS.INTEGER, "-" + tokenValue));
                    }
                    break;
            }

            //implementing solution for:
            // (*) (-|+) number
            // (/) (-|+) number
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].GetType() == Globals.SUPPORTED_TOKENS.MULTIPLICATION || tokens[i].GetType() == Globals.SUPPORTED_TOKENS.DIVISION)
                {
                    if (tokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.MINUS)
                    {
                        string tokenValue = tokens[(i + 2)].GetValue();
                        tokens.RemoveRange((i + 1), 2);
                        tokens.Insert((i + 1), new Token(Globals.SUPPORTED_TOKENS.INTEGER, "-" + tokenValue));
                    }
                    else if (tokens[(i + 1)].GetType() == Globals.SUPPORTED_TOKENS.PLUS)
                    {
                        string tokenValue = tokens[(i + 2)].GetValue();
                        tokens.RemoveRange((i + 1), 2);
                        tokens.Insert((i + 1), new Token(Globals.SUPPORTED_TOKENS.INTEGER, tokenValue));
                    }
                }
            }
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
            int left = index - 1;
            int right = index + 1;

            //Lists to concatenate
            List<Token> openBracket = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.OPEN_BRACKET, "(") };
            List<Token> leftSide = list[left];
            List<Token> rightSide = list[right];
            List<Token> op = list[index];
            List<Token> closeBracket = new List<Token>() { new Token(Globals.SUPPORTED_TOKENS.CLOSE_BRACKET, ")") };

            openBracket.AddRange(leftSide);
            openBracket.AddRange(op);
            openBracket.AddRange(rightSide);
            openBracket.AddRange(closeBracket);

            list.RemoveRange(left, 3);
            list.Insert(left, openBracket);
            return index - 1;
        }

        /// <summary>
        /// Function to remove consecutive + and - operators.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private void combinePlusAndMinusOps(List<Token> tokens)
        {
            int count = 0;
            int multiplier = 1;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].GetType() == Globals.SUPPORTED_TOKENS.PLUS || tokens[i].GetType() == Globals.SUPPORTED_TOKENS.MINUS)
                {
                    if (tokens[i].GetType() == Globals.SUPPORTED_TOKENS.PLUS)
                    {
                        multiplier *= 1;
                    }
                    else
                    {
                        multiplier *= -1;
                    }
                    count++;
                }
                else
                {
                    if (count != 0)
                    {
                        //Remove all the tokens
                        tokens.RemoveRange((i - count), count);
                        //then insert one that evaluates to the same value as all of those tokens
                        if (multiplier == 1)
                        {
                            tokens.Insert((i - count), new Token(Globals.SUPPORTED_TOKENS.PLUS, "+"));
                        }
                        else
                        {
                            tokens.Insert((i - count), new Token(Globals.SUPPORTED_TOKENS.MINUS, "-"));
                        }
                        i = i - count;
                        count = 0;
                        multiplier = 1;
                    }
                }
            }
        }
    }
}
