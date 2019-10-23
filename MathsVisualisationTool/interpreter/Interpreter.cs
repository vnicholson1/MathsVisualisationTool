using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    class Interpreter
    {

        private Lexer lexer;
        private Parser parser;

        public Interpreter()
        {
            lexer = new Lexer();
            parser = new Parser();
        }

        public string RunInterpreter(string codeToRun)
        {
            //Use the lexer to tokenise the input
            List<Token> tokens = lexer.TokeniseInput(codeToRun);
            //Check if the syntax is okay - throws an error if not and 
            //runs the code.
            parser.AnalyseTokens(tokens);

            //run the code

            return "yes";
        }

    }
}
