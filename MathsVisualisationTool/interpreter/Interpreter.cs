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
        }

        public string RunInterpreter(string codeToRun)
        {
            //Use the lexer to tokenise the input
            List<Token> tokens = lexer.TokeniseInput(codeToRun);
            parser = new Parser(tokens);
            parser.AnalyseTokens();

            //run the code

            return "yes";
        }

    }
}
