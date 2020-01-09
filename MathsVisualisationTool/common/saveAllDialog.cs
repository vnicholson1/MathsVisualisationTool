using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MathsVisualisationTool
{
    public partial class SaveAllDialog
    {
        public SaveAllDialog()
        {
            InitializeComponent();
        }

        /*
         * OnBrowse_Clicked - Handle event if the Okay button is 
         *                  clicked on the error message
         */
        private void OnBrowse_Clicked(object sender, RoutedEventArgs e)
        {
            
        }

        /*
         * OnSaveAll_Clicked - Handle event if the Okay button is 
         *                  clicked on the error message
         */
        private void OnSaveAll_Clicked(object sender, RoutedEventArgs e)
        {
            
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
