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

        public VariableJSONObject(string name, string value)
        {
            this.name = name;
            this.value = value;
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
                string name = (string) o;

                object valueObj = vars[name];

                string value = (string) valueObj;

                jsonObjects.Add(new VariableJSONObject(name, value));
            }

            return jsonObjects;
        }

    }
}
