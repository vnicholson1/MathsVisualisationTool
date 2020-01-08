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

        /************************************** STANDARD TOP MENU FUNCTIONS *********************************/
        #region TopMenuFunctions

        /*
         * OnOkay_Clicked - Handle event if the Okay button is 
         *                  clicked on the error message
         */
        private void OnClose_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*
         * OnResize_Clicked - Handle event if the Resize button is 
         *                  clicked from the View Menu or title bar.
         */
        private void OnResize_Clicked(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        /*
         * OnMinimise_Clicked - Handle event if the Minimise button is 
         *                      clicked from the View Menu or title bar.
         */
        private void OnMinimise_Clicked(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        /******************************** POPUP FUNCTIONS FOR DOCUMENTS HELP ********************************/
        #region DocHelpPopup
        private void OnShowDocPopup_Clicked(object sender, RoutedEventArgs e)
        {
            DocHelp_Popup.IsOpen = true;
        }

        private void OnHideDocPopup_Clicked(object sender, RoutedEventArgs e)
        {
            DocHelp_Popup.IsOpen = false;
        }
        #endregion

        /********************************** POPUP FUNCTIONS FOR VIDEO HELP **********************************/
        #region VideoHelpPopup
        private void OnShowVidPopup_Clicked(object sender, RoutedEventArgs e)
        {
            VidHelp_Popup.IsOpen = true;
        }

        private void OnHideVidPopup_Clicked(object sender, RoutedEventArgs e)
        {
            VidHelp_Popup.IsOpen = false;
        }
        #endregion

        /*********************************** FUNCTIONS FOR VIDEO PLAYER *************************************/
        #region VideoPlayerFunctions
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
        #endregion

        /*************************** FUNCTIONS TO CALL VIDEOS FROM VIDEO LIBRARY ****************************/
        #region VideoFunctions
        /*
         * OnBasicVid_Clicked -  
         */
        private void OnBasicVid_Clicked(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Source = new Uri("../../manuals/clips/basicInput_V1.mpg", UriKind.Relative);
        }
        #endregion

        /************************* FUNCTIONS TO CALL DOCS FROM DOCUMENTS LIBRARY ****************************/
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
    }
}