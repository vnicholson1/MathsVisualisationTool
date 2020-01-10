using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MathsVisualisationTool
{
    public partial class SaveAllDialog
    {

        FolderBrowserDialog folderBrowseDialog = null;
        MainWindow w;

        public SaveAllDialog(MainWindow w)
        {
            InitializeComponent();
            folderBrowseDialog = new FolderBrowserDialog();
            this.w = w;
        }

        /*
         * OnBrowse_Clicked - Handle event if the Okay button is 
         *                  clicked on the error message
         */
        private void OnBrowse_Clicked(object sender, RoutedEventArgs e)
        {
            if (folderBrowseDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ChosenDirectory.Text = folderBrowseDialog.SelectedPath;
            }
        }

        /*
         * OnSaveAll_Clicked - Handle event if the Okay button is 
         *                  clicked on the error message
         */
        private void OnSaveAll_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                string curDay = DateTime.Now.Day.ToString();
                if (curDay.Length == 1)
                {
                    curDay = "0" + curDay;
                }
                string curMonth = DateTime.Now.Month.ToString();
                if(curMonth.Length == 1)
                {
                    curMonth = "0" + curMonth;
                }
                string curYear = DateTime.Now.Year.ToString();
                string date = curDay + "-" + curMonth + "-" + curYear;
                string newName = "SolveIt " + date;
                string newDirectory = Path.Combine(ChosenDirectory.Text, newName);  

                Directory.CreateDirectory(newDirectory);

                //For this, save each 4 files individually
                if((bool)workshopCheck.IsChecked)
                {
                    Saver.saveWorkshop(w, Path.Combine(newDirectory, date + " " + "Your Numerical Workshop.txt"));
                } 
                if((bool)canvasCheck.IsChecked)
                {
                    Saver.saveCanvasGraphOntoExternalFile(w, Path.Combine(newDirectory, date + " " + "Your Graph Canvas.png"));
                }
                if((bool)lvcCheck.IsChecked)
                {
                    Saver.saveLiveChartsToExternalFile(w, Path.Combine(newDirectory, date + " " + "Your Live Charts.png"));
                }
                if((bool)varCheck.IsChecked)
                {
                    Saver.saveVariablesIntoExternalFile(Path.Combine(newDirectory, date + " " + "Your Variables.json"));
                }
                
                this.Close();
            } catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                UnknownErrorException u = new UnknownErrorException("An unknown error has occured - make sure that the live charts AND canvas graph tabs have been rendered before saving and that the directory given is correct.");
                ErrorMsg e2 = new ErrorMsg(u.Message, u.ErrorCode);
                e2.ShowDialog();
            }
            
        }

        /*
         * OnCancel_Clicked - Handle event if the Cancel button is 
         *                  clicked on the error message
         */
        private void OnCancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
