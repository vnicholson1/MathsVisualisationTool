using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    class VariableFileHandle
    {
        /// <summary>
        /// Method called to read in the variables.json file and gather the known variables.
        /// </summary>
        /// <returns> A hashmap of the form:
        /// key -> Variable name.
        /// value -> Variable value.
        /// </returns>
        public static Hashtable getVariables()
        {
            JArray variables = LoadVariableFile();
            Hashtable vars = new Hashtable();

            // Put all the variables loaded into a hashtable of the form
            // key -> Name
            // value -> Tuple( Value, Type).
            foreach(JObject variable in variables)
            {
                string variableName = variable["name"].ToString();
                string variableValue = variable["value"].ToString();

                vars.Add(variableName, variableValue);
            }

            return vars;
        }

        /// <summary>
        /// Method to clear all known variables. This will be called by typing in clear?
        /// </summary>
        public static void clearVariables()
        {
            saveVariables(new Hashtable());
        }

        /// <summary>
        /// Method to save the gathered variables into the variables file.
        /// </summary>
        public static void saveVariables(Hashtable vars)
        {
            WriteToVariableFile(vars);
        }

        /// <summary>
        /// Function to load the variable json file.
        /// </summary>
        /// <returns></returns>
        private static JArray LoadVariableFile()
        {
            //Get current WORKING directory (i.e. \bin\debug)
            string workingDirectory = Directory.GetCurrentDirectory();

            //Get PROJECT directory 
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            string filePath = Path.GetFullPath(Path.Combine(projectDirectory + "\\variables\\variables.json"));
            JArray arrayOfVars = JArray.Parse(File.ReadAllText(filePath));

            return arrayOfVars;
        }

        /// <summary>
        /// Method for writing the collected variables into the variables.json file.
        /// </summary>
        /// <param name="vars"></param>
        private static void WriteToVariableFile(Hashtable vars)
        {
            //Get current WORKING directory (i.e. \bin\debug)
            string workingDirectory = Directory.GetCurrentDirectory();

            //Get PROJECT directory 
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            string filePath = Path.GetFullPath(Path.Combine(projectDirectory + "\\variables\\variables.json"));

            //Convert the hashtable into an array of VariableJSONObjects.
            List<VariableJSONObject> jsonObjects = VariableJSONObject.convert(vars);

            //Convert into a JSON string
            string output = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
            
            //Then save it into variables.json.
            File.WriteAllText( filePath, output);
        }
    }
}
