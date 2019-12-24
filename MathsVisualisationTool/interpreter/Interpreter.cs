using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    class Interpreter
    {

        private Lexer lexer;
        private Parser parser;
        private Hashtable vars;

        public Interpreter()
        {
            lexer = new Lexer();
            vars = VariableFileHandle.getVariables();
            parser = new Parser(vars);
        }

        /// <summary>
        /// Method to run the interpreter given a certain input.
        /// </summary>
        /// <param name="codeToRun"></param>
        /// <returns></returns>
        public string RunInterpreter(string codeToRun)
        {
            string temp = Regex.Replace(codeToRun, @"\s+", "");

            //Check if the user has typed "clear"
            if(temp.Contains("clear"))
            {
                if(temp == "clear")
                {
                    VariableFileHandle.clearVariables();
                    return "\tCleared Variables.";
                } else
                {
                    throw new SyntaxErrorException("'clear' can only be used on its own, nothing else can be added.");
                }
            }


            //Use the lexer to tokenise the input
            List<Token> tokens = lexer.TokeniseInput(codeToRun);
            //Then put the gathered tokens into the parser.
            double result = parser.AnalyseTokens(tokens);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //VERY HACKY SOLUTION - WILL OPTIMISE IN LATER COMMITS.
            if(double.IsNaN(result))
            {
                if(parser.varName is null)
                {
                    return "\tRefer to figure.";
                }

                //this means a variable assignment has occured because no value has been returned (double.NaN).
                return "\tvar " + parser.varName + " = \n\t\t" + vars[parser.varName];
                //If varName is null then the plot function has been called (which does not return a value). 
            } else
            {
                //No variable has been assigned because a value has been returned.
                string res = Convert.ToString(result);

                vars["Ans"] = res;

                VariableFileHandle.saveVariables(vars);

                return "\tvar Ans = \n\t\t" + Convert.ToString(result);
            }
            
        }
    }
}
