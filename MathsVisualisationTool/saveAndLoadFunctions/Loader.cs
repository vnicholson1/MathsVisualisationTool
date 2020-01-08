using Microsoft.Win32;
using System;
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
                var varFile = openfileDialog.FileName;
            }
            // LOAD IN JSON HERE
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
