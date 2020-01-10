using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

                //Display a warning before proceeding.
                CustomMsg c = new CustomMsg("Are you sure you want to continue? This will override all variables currently stored in the table.", "Load Variable File");
                c.ShowDialog();
                bool? res = c.DialogResult;
                if(!((bool) res))
                {
                    return;
                }

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
                var filename = openfileDialog.FileName;

                //Display a warning before proceeding.
                CustomMsg c = new CustomMsg("Are you sure you want to continue? This will override everything currently in the Numerical Workshop.", "Load Text File");
                c.ShowDialog();
                bool? res = c.DialogResult;
                if (!((bool)res))
                {
                    return;
                }

                try
                {
                    string[] lines = File.ReadAllLines(filename);
                    w.Results.Items.Clear();

                    string lineToAdd = "";
                    bool flag = false;
                    for (int i=0;i<lines.Length;i++)
                    {
                        string line = lines[i];

                        if (line.Contains(">>>") || line.Contains("Refer to figure."))
                        {
                            w.Results.Items.Add(line);
                        } else
                        {
                            if (flag)
                            {
                                lineToAdd += line;
                                w.Results.Items.Add(lineToAdd);
                                lineToAdd = "";
                                flag = false;
                            } else
                            {
                                lineToAdd += line + "\n";
                                flag = true;
                            }
                        }
                    }

                } catch(Exception e)
                {
                    ErrorLoadingNumericalWorkshopFileException e1 = new ErrorLoadingNumericalWorkshopFileException("Error loading text file.");
                    ErrorMsg err = new ErrorMsg(e1.Message, e1.ErrorCode);
                    err.ShowDialog();
                }
            }
        }
    }
}
