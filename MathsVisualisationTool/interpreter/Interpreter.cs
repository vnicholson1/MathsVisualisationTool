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

        public Interpreter(ref LiveChartsDrawer l)
        {
            lexer = new Lexer();
            vars = VariableFileHandle.getVariables();
            parser = new Parser(vars, ref l);
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
                    throw new ClearCommandException("'clear' can only be used on its own, nothing else can be added.");
                }
            }


            //Use the lexer to tokenise the input
            List<Token> tokens = lexer.TokeniseInput(codeToRun);
            //Then put the gathered tokens into the parser.
            double result = parser.AnalyseTokens(tokens);

            switch(parser.status)
            {
                case Parser.STATUSES.PLOT_FUNCTION_CALLED:
                    return "\tRefer to figure.";

                case Parser.STATUSES.UNASSIGNED_RESULT:

                    string res = Convert.ToString(result);
                    //assign it to Ans as that is the default variable name.
                    vars["Ans"] = res;

                    VariableFileHandle.saveVariables(vars);

                    return "\tvar Ans = \n\t\t" + Convert.ToString(result);

                case Parser.STATUSES.VARIABLE_ASSIGNED:
                    return "\tvar " + parser.varName + " = \n\t\t" + vars[parser.varName];

                default:
                    return "";
            }
        }
    }
}
