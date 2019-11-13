using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    /// <summary>
    /// Class for storing a variable as a JSON object so that it can be serialized
    /// as a JSON object.
    /// </summary>
    class VariableJSONObject
    {
        //Fields must be public so that they can be seralized.
        public string name;
        public string value;
        public string type;

        public VariableJSONObject(string name, string value, string type)
        {
            this.name = name;
            this.value = value;
            this.type = type;
        }

        /// <summary>
        /// Method to convert the hashtable of variables into a VariableJSONObject list 
        /// so that it can be serialized for saving.
        /// </summary>
        /// <param name="vars"></param>
        /// <returns></returns>
        public static List<VariableJSONObject> convert(Hashtable vars)
        {
            List<VariableJSONObject> jsonObjects = new List<VariableJSONObject>();
            foreach (object o in vars.Keys)
            {
                string variableName = (string)o;

                object values = vars[variableName];

                Tuple<string, string> tuple = (Tuple<string, string>)values;

                string variableValue = tuple.Item1;
                string variableType = tuple.Item2;

                jsonObjects.Add(new VariableJSONObject(variableName, variableValue, variableType));
            }

            return jsonObjects;
        }

    }
}
