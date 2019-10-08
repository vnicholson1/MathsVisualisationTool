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

        public Interpreter()
        {
            lexer = new Lexer();
        }

        public void runInterpreter(string codeToRun)
        {
            //Use the lexer to tokenise the input
            lexer.tokeniseInput(codeToRun);
        }

    }
}
