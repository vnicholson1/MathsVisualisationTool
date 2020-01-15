using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    public class PlotFunction
    {
        //Up to the user, more data points means a smoother curve but is more computationally intensive. Less
        //data points means a more stair-case like curve but is less computationally intensive.
        private static readonly uint NUM_DATA_POINTS = 100;
        //To record the position of the variable name in the range argument.
        public int varNameIndex;
        public List<Token> allTokens;
        //To record the current index. Purely for checking if anything is found after the plot function.
        public int curIndex;
        //List of parameters in the Plot Function
        public List<Token> Equation;
        public double Xmin;
        public double Xmax;
        public double Ymin;
        public double Ymax;
        public double inc;
        //List to store all the gathered data points.
        public List<DataPoint> dataPoints = new List<DataPoint>();
        //The variables given in the plot function.
        public string X = null;
        public string Y = null;

        private PlotFunction(List<Token> Equation, Token XminToken, Token XmaxToken, Token incToken, List<Token> allTokens)
        {
            this.Equation = Equation;
            Xmin = Convert.ToDouble(XminToken.GetValue());
            Xmax = Convert.ToDouble(XmaxToken.GetValue());
            inc = Convert.ToDouble(incToken.GetValue());
            this.allTokens = allTokens;
        }

        /// <summary>
        /// Method for given a set of tokens and the location of the plot token, create a Plot function object.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static PlotFunction plotFunctionHandle(List<Token> tokens, int i)
        {

            //List for storing the equation to be drawn i.e. "Y=X"
            List<Token> equationTokens = getEquation(tokens, ref i);
            //skip over the comma
            i++;
            //Evaluate the range expression i.e. (1<X<10) - it does this by getting Xmin, Xmax and the necessary increment.
            Token[] elements = evaluateRange(tokens, ref i);
            Token Xmin = elements[0];
            Token Xmax = elements[1];
            Token inc = elements[2];

            PlotFunction newPlot = new PlotFunction(equationTokens, Xmin, Xmax, inc,tokens);

            newPlot.curIndex = i;
            newPlot.varNameIndex = Convert.ToInt32(elements[3].GetValue());

            return newPlot;
        }

        /// <summary>
        /// Method for extracting the equation part of the parameter from the list of tokens. 
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static List<Token> getEquation(List<Token> tokens,ref int index)
        {

            List<Token> equationTokens = new List<Token>();
            //Skip over the plot and open bracket tokens.
            index += 2;

            if(tokens.Count < 2)
            {
                throw new MissingOpenBracketAfterPlotFunctionNameException("Missing open bracket after plot function declaration");
            }

            if (tokens[index-1].GetType() != Globals.SUPPORTED_TOKENS.OPEN_BRACKET)
            {
                throw new MissingOpenBracketAfterPlotFunctionNameException("Missing open bracket after plot function declaration");
            }

            //For recording if the loop can escape when finding a comma.
            List<bool> statusList = new List<bool>() { false };

            bool functionFound = false;
            bool hasFoundClosingBracket = false;

            try
            {
                while (!canEscape(statusList))
                {
                    if(Globals.keyWords.Contains(tokens[index].GetValue()))
                    {
                        functionFound = true;
                        //if the function found is one with 2 arguments don't escape the loop.
                        if (Globals.funcsWith2Args.Contains(tokens[index].GetValue()))
                        {
                            statusList.Add(false);
                        }
                    }

                    //if the comma has been found then set the status to true.
                    if(tokens[index].GetType() == Globals.SUPPORTED_TOKENS.COMMA)
                    {
                        updateList(ref statusList);
                    }

                    //To check if the user has put in the correct number of arguments for one argument functions.
                    if(tokens[index].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
                    {
                        hasFoundClosingBracket = true;
                    }
                    //If you find either < or > symbols then clearly something is wrong
                    if(tokens[index].GetType() == Globals.SUPPORTED_TOKENS.LESS_THAN
                        || tokens[index].GetType() == Globals.SUPPORTED_TOKENS.GREATER_THAN)
                    {
                        throw new TooLittleArgumentsException("Not enough arguments given in function declaration.");
                    }

                    //For situations like 'plot(y=x)'
                    if (tokens[index].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET && index+1 == tokens.Count)
                    {
                        throw new MissingRangeArgumentException("Missing range argument.");
                    }

                    equationTokens.Add(tokens[index]);
                    index++;
                }

                if(functionFound)
                {
                    if (!hasFoundClosingBracket)
                    {
                        throw new TooManyArgumentsException("This function only has one argument");
                    }
                }
                

                //Because the comma token at the end is added to the list and we don't want that.
                //we just want the equation.
                equationTokens.RemoveAt(equationTokens.Count - 1);
                index--;

                //For situations like 'plot(y=x,'
                if(index == (tokens.Count-1))
                {
                    throw new UnexpectedlyReachedEndOfExpressionException("Unexpectedly reached end of expression.");
                }

            } catch (ArgumentOutOfRangeException e)
            {
                //Occurs if someone was to put 'plot(y=x' as input.
                throw new UnexpectedlyReachedEndOfExpressionException("Unexpectedly reached end of expression.");
            }
            
            
            //Minimum length of an equation has to be 3 i.e. "Y=3".
            if(equationTokens.Count < 3)
            {
                throw new TooShortEquationException("Invalid equation given in plot function.");
            }

            return equationTokens;
        }

        /// <summary>
        /// Function for checking whether the listOfStatuses is true or not.
        /// Its only true if all the bools in the list are true.
        /// </summary>
        /// <param name="listOfStatuses"></param>
        /// <returns></returns>
        private static bool canEscape(List<bool> listOfStatuses)
        {
            foreach(bool b in listOfStatuses)
            {
                if(b is false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// If a comma has been found then only set one of the bools to true.
        /// </summary>
        /// <param name="listOfStatuses"></param>
        private static void updateList(ref List<bool> listOfStatuses)
        {
            int counter = 0;
            foreach(bool b in listOfStatuses)
            {
                if(b is false)
                {
                    listOfStatuses[counter] = true;
                    return;
                }
                counter++;
            }
        }

        /// <summary>
        /// Method for evaluating the given range.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns> An array of size 3 containing the Xmin, Xmax and the increment.</returns>
        private static Token [] evaluateRange(List<Token> tokens, ref int index)
        {
            //Temp variable for saving where the name of the dependent variable is specified.
            int varNameIndex = 0;

            //An array for storing the Xmin, Xmax, the increment and the position of the variable in the range argument.
            Token[] elements = new Token[4];

            //For situations like 'plot(y=x,)'
            if(tokens[index].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
            {
                throw new EmptyRangeException("Empty range argument found.");
            }

            //get the Xmin
            elements[0] = GetXMinToken(tokens, ref index);
            bool hasGreaterThanSymbol = false;


            //Not sure if above Exception is ever thrown lol 
            if(tokens[index].GetType() == Globals.SUPPORTED_TOKENS.GREATER_THAN)
            {
                hasGreaterThanSymbol = true;
            }

            //Check the next token is the dependent variable. 
            index++;
            if (tokens[index].GetType() != Globals.SUPPORTED_TOKENS.VARIABLE_NAME)
            {
                throw new IncorrectTypeInVariablePositionException("Variable name expected. " + tokens[index].GetValue() + " found instead.");
            } else
            {
                varNameIndex = index;
            }
            //Check the next token is a < or >
            index++;
            if (tokens[index].GetType() != Globals.SUPPORTED_TOKENS.LESS_THAN
                && tokens[index].GetType() != Globals.SUPPORTED_TOKENS.GREATER_THAN)
            {
                throw new InvalidTypeAfterVariableNameException("< symbol expected. " + tokens[index].GetValue() + " found instead.");
            } else
            {
                //Cannot mix < or > so check if the symbols are consistent
                if(tokens[index].GetType() == Globals.SUPPORTED_TOKENS.LESS_THAN)
                {
                    if(hasGreaterThanSymbol)
                    {
                        throw new MixedRangeOperatorsException("Cannot mix < or > symbols");
                    }
                } else if (tokens[index].GetType() == Globals.SUPPORTED_TOKENS.GREATER_THAN)
                {
                    if (!hasGreaterThanSymbol)
                    {
                        throw new MixedRangeOperatorsException("Cannot mix < or > symbols");
                    }
                }
            }
            index++;
            //get the Xmax
            elements[1] = GetXMaxToken(tokens, ref index);

            if (hasGreaterThanSymbol)
            {
                swap(ref elements);
            }

            //Calculate the increment to get the desired increment.
            double Xmin = Convert.ToDouble(elements[0].GetValue());
            double Xmax = Convert.ToDouble(elements[1].GetValue());

            if (!(Xmax > Xmin))
            {
                throw new XminGreaterThanXmaxException("Xmax must be greater than Xmin.");
            }

            double inc = (Xmax - Xmin + 1) / NUM_DATA_POINTS;
            elements[2] = new Token(Globals.SUPPORTED_TOKENS.CONSTANT, Convert.ToString( (Xmax - Xmin) / (NUM_DATA_POINTS-1)));

            //record the position of the variable in the range argument as a token because I couldn't think 
            //of any other way to do it.
            elements[3] = new Token(Globals.SUPPORTED_TOKENS.CONSTANT, Convert.ToString(varNameIndex));

            return elements;
        }

        /// <summary>
        /// Function for swapping round the Xmin and Xmax.
        /// </summary>
        /// <param name="Xmin"></param>
        /// <param name="Xmax"></param>
        private static void swap(ref Token[] elements)
        {
            Token temp = elements[0];

            elements[0] = elements[1];

            elements[1] = temp;
        }

        /// <summary>
        /// Private method to extract the Xmin from the range argument.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static Token GetXMinToken(List<Token> tokens, ref int index)
        {
            Parser p = new Parser();
            List<Token> XminList = new List<Token>();

            while (tokens[index].GetType() != Globals.SUPPORTED_TOKENS.LESS_THAN 
                && tokens[index].GetType() != Globals.SUPPORTED_TOKENS.GREATER_THAN)
            {

                XminList.Add(tokens[index]);
                index++;

                //For situations like 'plot(y=x,2'
                if (index == tokens.Count)
                {
                    throw new UnexpectedlyReachedEndOfExpressionException("Unexpectedly reached end of expression.");
                }
            }

            return new Token(Globals.SUPPORTED_TOKENS.CONSTANT, Convert.ToString(p.AnalyseTokens(XminList)));
        }

        /// <summary>
        /// Private method to extract the Xmin from the range argument.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static Token GetXMaxToken(List<Token> tokens, ref int index)
        {
            Parser p = new Parser();
            List<Token> XmaxList = new List<Token>();

            while (tokens[index].GetType() != Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
            {

                XmaxList.Add(tokens[index]);
                index++;

                //For situations like 'plot(y=x,2<x<3'
                if (index == tokens.Count)
                {
                    throw new UnexpectedlyReachedEndOfExpressionException("Unexpectedly reached end of expression.");
                }
            }

            return new Token(Globals.SUPPORTED_TOKENS.CONSTANT, Convert.ToString(p.AnalyseTokens(XmaxList)));
        }

        /// <summary>
        /// Method for getting the list of data points from the expression so they can be drawn by the graph drawer.
        /// Result stored in Plot Function object under data points.
        /// </summary>
        /// <returns></returns>
        public void getValues()
        {
            //Say if your expression is Y=X+2
            //First step is to remove the Y= part and then evaulate the X+2 part by parsing it through the parser.
            removeFirst2Tokens();

            //Next step is to get the variable name in the equation.
            string varName = getVariableName();

            Hashtable vars = VariableFileHandle.getVariables();

            //get the number of data values to plot.
            int numIncrements = Convert.ToInt32(Math.Floor((Xmax - Xmin) / inc) + 1);

            Parser p;
            bool flag = false;
            //Save the original equation.
            List<Token> eqnCopy = new List<Token>(Equation);
            //Iterate through every value of X and get a Y value.
            for (int i=0;i<numIncrements;i++)
            {
                double Xvalue = Xmin + i * inc;

                if(varName != null)
                {
                    //Update the value of the variable so that it can be used in the expression.
                    vars[varName] = Convert.ToString(Xvalue);
                    //Save the change
                    VariableFileHandle.saveVariables(vars);

                    p = new Parser(vars);

                    double Yvalue = p.AnalyseTokens(Equation);
                    //Copy the Equation over incase it has been modified.
                    Equation = new List<Token>(eqnCopy);

                    dataPoints.Add(new DataPoint(Xvalue, Yvalue));
                } else
                {
                    //This only occurs if only 1 variable has been specified.
                    p = new Parser(vars);

                    double Yvalue = p.AnalyseTokens(Equation);

                    //If the equation specified is 'X=3' or 'x=3' then you want a vertical line.
                    if(Y.ToLower() == "x")
                    {
                        flag = true;
                        dataPoints.Add(new DataPoint(Yvalue, Xvalue));
                    } else
                    {
                        //otherwise draw a horizontal line.
                        X = "x";
                        dataPoints.Add(new DataPoint(Xvalue, Yvalue));
                    }
                }
            }

            //Check if the dependent variable name in the equation matches the one given in the range.
            if (!(X is null))
            {
                if (allTokens[varNameIndex].GetValue() != X)
                {
                    throw new InvalidVariableNameInRangeException("Dependent variable " + X + " expected. " + allTokens[varNameIndex].GetValue() + " found instead.");
                }
            }

            if (flag)
            {
                X = Y;
                Y = "y";
            }

            setMinAndMaxXandYValues();

            printDataPoints();
        }

        /// <summary>
        /// Private method to get the variable name stored in the Equation list.
        /// Also updates all cases of that variable from 'y' to '~y' for example.
        /// </summary>
        /// <returns></returns>
        private string getVariableName()
        {
            string varName = null;

            for(int i=0;i<Equation.Count;i++)
            {
                if(Equation[i].GetType() == Globals.SUPPORTED_TOKENS.VARIABLE_NAME)
                {
                    if(X != null)
                    {
                        if(X != Equation[i].GetValue())
                        {
                            throw new TooManyVariablesException("Only a maximum of two variables can be declared in the equation.");
                        }
                    }

                    X = Equation[i].GetValue();

                    if(X == Y)
                    {
                        throw new DependentAndIndependentVariablesWithSameNameException("The dependent variable cannot have the same name as the independent variable.");
                    }

                    //prepend a '~' character onto the variable, so if someone wants to define a variable called Y, then it won't be overwritten.
                    Equation[i].SetValue("~" + Equation[i].GetValue());
                    varName = Equation[i].GetValue();
                }
            }

            return varName;
        }

        /// <summary>
        /// Method to remove the first 2 elements in the equation (i.e. the Y= part).
        /// </summary>
        private void removeFirst2Tokens()
        {
            if(Equation[0].GetType() != Globals.SUPPORTED_TOKENS.VARIABLE_NAME || 
                Equation[1].GetType() != Globals.SUPPORTED_TOKENS.ASSIGNMENT)
            {
                throw new MissingDependentVariableException("First two tokens in the equation must be 'var_name1' followed by '='.");
            }

            Y = Equation[0].GetValue();
            Equation.RemoveRange(0, 2);
        }

        /// <summary>
        /// Method to get the Min and Max X and Y values of the data points. (this is required by the Graph Drawer Module).
        /// </summary>
        private void setMinAndMaxXandYValues()
        {
            double maxTempY = double.MinValue;
            double minTempY = double.MaxValue;
            double maxTempX= double.MinValue;
            double minTempX = double.MaxValue;
            foreach (DataPoint d in dataPoints)
            {
                if(d.getY() > maxTempY)
                {
                    maxTempY = d.getY();
                }
                if(d.getY() < minTempY)
                {
                    minTempY = d.getY();
                }

                if (d.getX() > maxTempX)
                {
                    maxTempX = d.getX();
                }
                if (d.getX() < minTempX)
                {
                    minTempX = d.getX();
                }
            }
            Ymin = minTempY;
            Ymax = maxTempY;
            Xmin = minTempX;
            Xmax = maxTempX;

            //Dealing with situations like 'Y=2'
            if(minTempY == maxTempY)
            {
                Ymin = minTempY - 1;
                Ymax = maxTempY + 1;
            }

            //Dealing with situations like 'X=2'
            if (minTempX == maxTempX)
            {
                Xmin = minTempX - 1;
                Xmax = maxTempX + 1;
            }
        }

        /// <summary>
        /// Print out the list of gathered data points.
        /// Mainly for debugging purposes.
        /// </summary>
        /// <param name="dataPoints"></param>
        private void printDataPoints()
        {
            foreach (DataPoint d in dataPoints)
            {
                Console.WriteLine("(" + d.getX() + "," + d.getY() + ")");
            }
        }
    }
}
