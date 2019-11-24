using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    class PlotFunction
    {
        private List<Token> Equation;
        private double Xmin;
        private double Xmax;
        private double inc;

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

            //Check the syntax first.

            //List for storing the equation to be drawn i.e. "Y=X"
            List<Token> equationTokens = new List<Token>();

            //Skip over the plot and open bracket tokens.
            i += 2;
            while (tokens[i].GetType() != Globals.SUPPORTED_TOKENS.COMMA)
            {
                if (tokens[i].GetType() == Globals.SUPPORTED_TOKENS.CLOSE_BRACKET)
                {
                    throw new SyntaxErrorException("Missing Xmin, Xmax and increment arguments.");
                }
                equationTokens.Add(tokens[i]);
                i++;
            }
            //Current token will be the comma so skip over it to get the next argument.
            //get the next argument which is Xmin.
            i++;
            Token Xmin = tokens[i];
            //get the next argument which is Xmax.
            i++;
            //skip over the comma.
            i++;
            Token Xmax = tokens[i];
            //get the next argument which is inc.
            i++;
            //skip over the comma.
            i++;
            Token inc = tokens[i];

            return new PlotFunction(equationTokens, Xmin, Xmax, inc);
        }

        /// <summary>
        /// Function for checking the syntax of all parameters given.
        /// </summary>
        private void checkSyntax()
        {
            throw new NotImplementedException("Not implemented yet.");
        }

        /// <summary>
        /// Method for getting the list of values from the expression so they can be drawn by the graph drawer.
        /// </summary>
        /// <returns></returns>
        public List<Tuple<double,double>> getValues()
        {
            //Say if your expression is Y=X+2
            //First step is to remove the Y= part and then evaulate the X+2 part by parsing it through the parser.
            removeFirst2Tokens();

            //Next step is to get the variable name in the equation.
            string varName = getVariableName();

            Hashtable vars = VariableFileHandle.getVariables();

            //get the number of data values to plot.
            int numIncrements = Convert.ToInt32(Math.Floor((Xmax - Xmin) / inc) + 1);

            //List to store all the gathered data points.
            List<Tuple<double, double>> dataPoints = new List<Tuple<double, double>>();
            Parser p;
            //Iterate through every value of X and get a Y value.
            for(int i=0;i<numIncrements;i++)
            {
                double Xvalue = Xmin + i * inc;

                vars[varName] = Convert.ToString(Xvalue);

                p = new Parser(vars);

                double Yvalue = p.AnalyseTokens(Equation);

                dataPoints.Add(new Tuple<double, double>(Xvalue, Yvalue));
            }

            printDataPoints(dataPoints);
            return dataPoints;
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
            Equation.RemoveRange(0, 2);
        }

        /// <summary>
        /// Print out the list of gathered data points.
        /// Mainly for debugging purposes.
        /// </summary>
        /// <param name="dataPoints"></param>
        private void printDataPoints(List<Tuple<double, double>> dataPoints)
        {
            foreach (Tuple<double, double> t in dataPoints)
            {
                Console.WriteLine("(" + t.Item1 + "," + t.Item2 + ")");
            }
        }
    }
}
