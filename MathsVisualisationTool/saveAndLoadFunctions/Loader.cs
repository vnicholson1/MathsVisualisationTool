using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathsVisualisationTool
{
    public static class Loader
    {
        /// <summary>
        /// Function to load the JSON file given by the user into the datagrid.
        /// </summary>
        /// <param name="w"></param>
        public static void loadVarFileIntoDataGrid(MainWindow w)
        {
            var openfileDialog = new OpenFileDialog
            {
                Filter = "JSON File (*.json)|*.json"
            };
            var dialogResult = openfileDialog.ShowDialog();
            if (dialogResult == true)
            {
                var filename = openfileDialog.FileName;
                JArray array = null; 

                try
                {
                    array = VariableFileHandle.LoadFromExternalFile(filename);

                    Hashtable vars = new Hashtable();

                    // Put all the variables loaded into a hashtable of the form
                    // key -> Name
                    // value -> Tuple( Value, Type).
                    foreach (JObject variable in array)
                    {
                        string variableName = variable["name"].ToString();
                        string variableValue = variable["value"].ToString();

                        vars.Add(variableName, variableValue);
                    }

                    VariableFileHandle.saveVariables(vars);

                    w.loadVarsIntoDataGrid();

                } catch(Exception e)
                {
                    ErrorLoadingVariableFileException e1 = new ErrorLoadingVariableFileException("Error loading variable file.");
                    ErrorMsg err = new ErrorMsg(e1.Message,e1.ErrorCode);
                    err.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Function to load in an external text file into the list box.
        /// </summary>
        /// <param name="w"></param>
        public static void loadTextFileIntoNumericalWorkshop(MainWindow w)
        {
            var openfileDialog = new OpenFileDialog
            {
                Filter = "Text File (*.txt)|*.txt"
            };
            var dialogResult = openfileDialog.ShowDialog();
            if (dialogResult == true)
            {
                var varFile = openfileDialog.FileName;
            }

            // LOAD IN LISTBOX HERE
        }
    }
}
