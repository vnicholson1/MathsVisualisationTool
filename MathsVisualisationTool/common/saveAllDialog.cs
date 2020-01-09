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
                //For this, save each 4 files individually
                Saver.saveWorkshop(w, Path.Combine(ChosenDirectory.Text, "workshop.txt"));
                Saver.saveCanvasGraphOntoExternalFile(w, Path.Combine(ChosenDirectory.Text, "canvas.png"));
                Saver.saveLiveChartsToExternalFile(w, Path.Combine(ChosenDirectory.Text, "liveCharts.png"));
                Saver.saveVariablesIntoExternalFile(Path.Combine(ChosenDirectory.Text, "vars.json"));
                this.Close();
            } catch (Exception err)
            {
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
