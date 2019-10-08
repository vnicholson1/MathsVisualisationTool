using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MathsVisualisationTool
{
    class Lexer
    {
        public Lexer()
        {

        }

        public string tokeniseInput(string input)
        {
            JToken config = loadInterpreterConfig();

            JToken [] supportedTypes = config["SUPPORTED_TYPES"].ToArray();

            Console.WriteLine(supportedTypes[0].ToString());
            return "yes";
        }

        /*
         * Function to load the interpreter's configuration file stored in config.
         */
        private JToken loadInterpreterConfig()
        {
            //Get current WORKING directory (i.e. \bin\debug)
            string workingDirectory = Directory.GetCurrentDirectory();

            //Get PROJECT directory 
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            string filePath = Path.GetFullPath(Path.Combine(projectDirectory + "\\config\\InterpreterConfig.json"));
            JObject jsonObject = JObject.Parse(File.ReadAllText(filePath));

            JToken con = jsonObject["INTERPRETER_CONFIG"];

            return con; 
        }
    }

    class Token
    {
        private string type;
        private int value;

        public Token(string type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
