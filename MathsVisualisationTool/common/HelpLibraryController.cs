using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;

namespace MathsVisualisationTool
{
    public partial class HelpLibrary : Window
    {

        public HelpLibrary()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (VideoPlayer.Source != null)
            {
                if (VideoPlayer.NaturalDuration.HasTimeSpan)
                    lblStatus.Content = String.Format("{0} / {1}", VideoPlayer.Position.ToString(@"mm\:ss"), VideoPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
                lblStatus.Content = "No file selected...";
        }

        private void OnPlay_Clicked(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Play();
        }

        private void OnPause_Clicked(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Pause();
        }

        /****************************************************************************************************/
        /*********************************** FUNCTIONS FOR VIDEO LIBRARY ************************************/
        #region VideoFunctions
        /*
         * 
         * private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
        if(openFileDialog.ShowDialog() == true)
            mePlayer.Source = new Uri(openFileDialog.FileName);
    }
    */

    /*
     * OnBasicVid_Clicked -  
     */
        private void OnBasicVid_Clicked(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Source = new Uri(@"C:\Users\andy\Documents\GitHub\MathsVisualisationTool\manuals\clips\basicInput_V1.mpg");
        }
        #endregion
        /******************************* END OF FUNCTIONS FOR VIDEO LIBRARY *********************************/

        /******************************** FUNCTIONS FOR DOCUMENTS LIBRARY ***********************************/
        #region DocsFunctions
        /*
         * OnInputDoc_Clicked -  
         */
        private void OnInputDoc_Clicked(object sender, RoutedEventArgs e)
        {
            XpsDocument testDocument = new XpsDocument("../../manuals/documentation/testDoc.xps", FileAccess.Read);
            libraryDocViewer.Document = testDocument.GetFixedDocumentSequence();
        }
        #endregion
        /***************************** END OF FUNCTIONS FOR DOCUMENTS LIBRARY *******************************/
    }
}