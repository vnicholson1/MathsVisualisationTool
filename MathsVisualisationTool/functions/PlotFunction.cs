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

        public PlotFunction(List<Token> Equation, Token XminToken, Token XmaxToken, Token incToken)
        {
            this.Equation = Equation;
            Xmin = Convert.ToDouble(XminToken.GetValue());
            Xmax = Convert.ToDouble(XmaxToken.GetValue());
            inc = Convert.ToDouble(incToken.GetValue());
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

            PlotFunction newPlot = new PlotFunction(equationTokens, Xmin, Xmax, inc);

            newPlot.curIndex = i;

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
            while (tokens[index].GetType() != Globals.SUPPORTED_TOKENS.COMMA)
            {
                if (tokens[index].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
                {
                    throw new SyntaxErrorException("Missing range argument.");
                }
                equationTokens.Add(tokens[index]);
                index++;
            }
            
            //Minimum length of an equation has to be 3 i.e. "Y=3".
            if(equationTokens.Count < 3)
            {
                throw new SyntaxErrorException("Invalid equation given in plot function.");
            }

            return equationTokens;
        }

        /// <summary>
        /// Method for evaluating the given range.
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="index"></param>
        /// <returns> An array of size 3 containing the Xmin, Xmax and the increment.</returns>
        private static Token [] evaluateRange(List<Token> tokens, ref int index)
        {
            //An array for storing the Xmin, Xmax and the increment
            Token[] elements = new Token[3];

            //get the Xmin
            elements[0] = tokens[index];
            index += 4;
            //get the Xmax
            elements[1] = tokens[index];
            //Calculate the increment to get the desired increment.
            double Xmin = Convert.ToDouble(elements[0].GetValue());
            double Xmax = Convert.ToDouble(elements[1].GetValue());

            double inc = (Xmax - Xmin + 1) / NUM_DATA_POINTS;
            elements[2] = new Token(Globals.SUPPORTED_TOKENS.CONSTANT, Convert.ToString( (Xmax - Xmin) / (NUM_DATA_POINTS-1)));

            //So that index points to the closing bracket of the plot function.
            index += 1;
            return elements;
        }

        /// <summary>
        /// Method for getting the list of values from the expression so they can be drawn by the graph drawer.
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
            //Iterate through every value of X and get a Y value.
            for (int i=0;i<numIncrements;i++)
            {
                double Xvalue = Xmin + i * inc;

                if(varName != null)
                {
                    //Update the value of the variable so that it can be used in the expression.
                    vars[varName] = Convert.ToString(Xvalue);

                    p = new Parser(vars);

                    double Yvalue = p.AnalyseTokens(Equation);

                    dataPoints.Add(new DataPoint(Xvalue, Yvalue));
                } else
                {
                    //This only occurs if only 1 variable has been specified.

                    p = new Parser(vars);

                    double Yvalue = p.AnalyseTokens(Equation);

                    //If the equation specified is 'X=3' then you want a vertical line.
                    if(Y.ToLower() == "x")
                    {
                        flag = true;
                        dataPoints.Add(new DataPoint(Yvalue, Xvalue));
                    } else
                    {
                        X = "x";
                        dataPoints.Add(new DataPoint(Xvalue, Yvalue));
                    }
                }
            }

            if(flag)
            {
                X = "x";
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
                        throw new SyntaxErrorException("Only a maximum of two variables can be declared in the equation.");
                    }

                    X = Equation[i].GetValue();

                    if(X == Y)
                    {
                        throw new SyntaxErrorException("The dependent variable cannot have the same name as the independent variable.");
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
                throw new SyntaxErrorException("First two tokens in the equation must be 'var_name1' followed by '='.");
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
