using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Xps.Packaging;
using System.IO;
using System.Windows.Threading;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Wpf;

namespace MathsVisualisationTool
{

    public partial class MainWindow : Window
    {
        public string InputBoxValue
        {
            get { return inputBox.Text; }
            set { inputBox.Text = value; }
        }

        //Fields for the MainWindow Class.
        LiveChartsDrawer l;
        //Stop Watch Variables.
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;

        /// <summary>
        /// Constructor for the MainWindow object.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            inputBox.KeyDown += new KeyEventHandler(InputBox_KeyDown);
            /**************************************** DATAGRID FUNCTIONS ************************************/
            #region DataGrid
            //To load the variables into the datagrid.
            loadVarsIntoDataGrid();
            var column = new DataGridTextColumn();
            #endregion
            /************************************************************************************************/
            /**************************************** CLOCK FUNCTIONS ***************************************/
            #region Clock
            dt.Tick += new EventHandler(MainStopwatch);
            dt.Tick += new EventHandler(MainClock);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            //dt.Interval = TimeSpan.FromSeconds(1);
            dt.Start();
            #endregion
            /************************************************************************************************/
            /************************************* LIVE CHART FUNCTIONS *************************************/
            #region LiveCharts
            l = new LiveChartsDrawer(this);
            l.Draw();
            this.DataContext = l;

            //////////////////////////////////////////////////////////////////////////////////////////////////
            ///To change the colour of the tooltip - LvChrt is an object pointing to the LiveCharts container.
            ///https://lvcharts.net/App/examples/v1/wf/Tooltips%20and%20Legends
            LvChrt.DataTooltip.Background = Brushes.Black;
            #endregion
            /********************************* END OF LIVE CHART FUNCTIONS **********************************/
        }

        /*
         * OnNewNote_Clicked - Handle event if Add New Note button is 
         *                  clicked from the Notes Popout Pane.
         */
        private void OnNewNote_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add New Note - Fix IT!!!!");
        }

        /****************************************TO WORK ON************************************************************/


        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /****************************************************************************************************/

        /************************ CLOCK FUNCTIONS (INC. CLOCK/TIMER/STOPWATCH/CALENDAR) *********************/
        #region ClockFunctions

        void MainStopwatch(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                sWatchLabel.Content = currentTime;
            }
        }

        /*
         * MainClock -  Handle event for the digital clock feature
         *              which displays current time
         */
        void MainClock(object sender, EventArgs e)
        {
            clockTime.Content = DateTime.Now.ToLongTimeString();
            currentDay.Content = DateTime.Now.DayOfWeek;
            dateTime.Content = DateTime.Now.ToLongDateString();
        }

        /*
         * ResetStopwatch_Clicked - Handle event if Starts Stopwatch button is 
         *                          clicked, starts the stopwatch running
         */
        private void StartStopwatch_Clicked(object sender, EventArgs e)
        {
            stopWatch.Start();
        }

        /*
         * StopStopwatch_Clicked - Handle event if Stop Stopwatch button is 
         *                          clicked, stops the stopwatch if running
         */
        private void StopStopwatch_Clicked(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
                stopWatch.Stop();
        }

        /*
         * LapStopwatch_Clicked - Handle event if Lap Stopwatch button is 
         *                        clicked, records split/lap time to list
         */
        private void LapStopwatch_Clicked(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
                lapTimes_List.Items.Add(currentTime);
        }

        /*
         * ResetStopwatch_Clicked - Handle event if Reset Stopwatch button is 
         *                          clicked, resets the stopwatch to zero
         */
        private void ResetStopwatch_Clicked(object sender, EventArgs e)
        {
            stopWatch.Reset();
            sWatchLabel.Content = "00:00:00:00";
        }

        /*
         * ClearLapStopwatch_Clicked -  Handle event if Clear button is 
         *                              clicked, clears all stored lap 
         *                              times
         */
        private void ClearLapStopwatch_Clicked(object sender, EventArgs e)
        {
            lapTimes_List.Items.Clear();
        }

        private int _timerHour = 0;
        public int timerHourValue
        {
            get { return _timerHour; }
            set { _timerHour = value; hoursLabel.Content = value.ToString(); }
        }

        /*
         * OnHourUp_Clicked -   Handle event if Increase Hour button is 
         *                      clicked, increases the hour label by 1 
         *                      per click
         */
        private void OnHourUp_Clicked(object sender, EventArgs e)
        {
            hoursLabel.Content = timerHourValue++;
        }


        /*
         * OnHourDown_Clicked -   Handle event if decreases Hour button is 
         *                      clicked, decreases the hour label by 1 
         *                      per click
         */
        private void OnHourDown_Clicked(object sender, EventArgs e)
        {
            hoursLabel.Content = timerHourValue--;
        }

        /*
         * OnMinUp_Clicked -    Handle event if Increase Minute button is 
         *                      clicked, increases the minute label by 1 
         *                      per click
         */
        private void OnMinUp_Clicked(object sender, EventArgs e)
        {

        }

        /*
         * OnMinDown_Clicked -   Handle event if decreases Min button is 
         *                      clicked, decreases the minute label by 1 
         *                      per click
         */
        private void OnMinDown_Clicked(object sender, EventArgs e)
        {

        }

        /*
         * OnSecUp_Clicked -   Handle event if Increase Second button is 
         *                      clicked, increases the seconds label by 1 
         *                      per click
         */
        private void OnSecUp_Clicked(object sender, EventArgs e)
        {

        }

        /*
         * OnSecDown_Clicked -   Handle event if decreases Sec button is 
         *                      clicked, decreases the sec label by 1 
         *                      per click
         */
        private void OnSecDown_Clicked(object sender, EventArgs e)
        {

        }
        #endregion
        /******************** END OF CLOCK FUNCTIONS (INC. CLOCK/TIMER/STOPWATCH/CALENDAR) ******************/
        /****************************** DRAG/DROP FUNCTIONS [NONE FUNCTIONAL ATM] ***************************/
        #region DragDropFunctions
        private void OnDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Button draggedBtn)
            {
                DragDrop.DoDragDrop(draggedBtn, draggedBtn, DragDropEffects.Copy);
            }
        }

        void onDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(e.Data.GetFormats()[0]) is Button droppedBtn)
            {
                functionPanel.Children.Remove(droppedBtn);
                favsPanel.Children.Add(droppedBtn);
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            IDataObject draggedData = NewMethod(e);
            if (draggedData.GetData(draggedData.GetFormats()[0]) is Button droppedBtn)
            {
                functionPanel.Children.Remove(droppedBtn);
                favsPanel.Children.Add(droppedBtn);
            }
        }

        private static IDataObject NewMethod(DragEventArgs e)
        {
            return e.Data;
        }
        #endregion
        /************************** END OF DRAG/DROP FUNCTIONS [NONE FUNCTIONAL ATM] ************************/
        /************************************ FUNCTIONS TO RUN/SUBMIT INPUT *********************************/
        #region RunSubmitFunctions
        /*
         * OnRun_Clicked - Handle event if the Run/Submit button is 
         *                  clicked.
         */
        private void OnRun_Clicked(object sender, RoutedEventArgs e)
        {
            //HandleTextEnter();
            if (this.inputBox.Text != "")
            {
                Results.Items.Add(">>> \t" + this.inputBox.Text);
                //Interpreter String called results
                Interpreter i = new Interpreter(ref l);
                //string output = i.RunInterpreter(inputBox.Text);
                try
                {
                    Results.Items.Add(i.RunInterpreter(inputBox.Text));
                    //Update the LiveChartsDrawer onto the mainWindow.
                    this.DataContext = l;
                } catch(Exception exp)
                {
                    Results.Items.Add(exp.Message);
                    MessageBox.Show(exp.Message);
                    Results.Items.Add("Error 2.1");
                }
                this.inputBox.Focus();
                this.inputBox.Clear();
                loadVarsIntoDataGrid();
            }
            else
            {
                MessageBox.Show("ERROR");
                this.inputBox.Focus();
            }
        }

        /*
         * InputBox_KeyDown - Handle event if the return button has 
         *                  been pressed.
         */
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //HandleTextEnter();
                e.Handled = true;
                if (this.inputBox.Text != " ")
                {
                    Results.Items.Add(">>> \t" + this.inputBox.Text);
                    //Interpreter String called results
                    Interpreter i = new Interpreter(ref l);
                    try
                    {
                        Results.Items.Add(i.RunInterpreter(inputBox.Text));
                    }
                    catch (Exception exp)
                    {
                        Console.WriteLine(exp.ToString());
                        //Results.Items.Add(exp.Message);
                        MessageBox.Show(exp.Message);
                        Results.Items.Add("Error 2.1");
                    }
                    this.inputBox.Focus();
                    this.inputBox.Clear();
                }
                else
                {
                    MessageBox.Show("ERROR");
                    this.inputBox.Focus();
                }
                loadVarsIntoDataGrid();
            }
        }

        /*
         * HandleTextEnter - Handle the text enter and put the contents 
         *                  of the text box into the interpreter.
         */
        private void HandleTextEnter()
        {
            string content = inputBox.Text;
            inputBox.Clear();

            Interpreter interp = new Interpreter(ref l);

            try
            {
                string output = interp.RunInterpreter(content);
                Console.WriteLine(output); //output onto the screen
            }
            catch (Exception e)
            {
                string output = e.ToString();
                Console.WriteLine(output); //output onto the screen but in red.
            }

        }
        #endregion
        /********************************* END OF FUNCTIONS TO RUN/SUBMIT INPUT *****************************/
        /************************************** STANDARD TOP MENU FUNCTIONS *********************************/
        #region TopMenuFunctions
        /*
         * OnExitMenuClicked - Handle event if the Exit button is 
         *                  clicked from the standard File Menu.
         */
        private void OnExitMenuClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Goodbye - Thankyou for using SolveIT!");
            Environment.Exit(0);
        }

        /*
         * OnTestDocClicked -  
         */
        private void OnTestDocClicked(object sender, RoutedEventArgs e)
        {
            XpsDocument testDocument = new XpsDocument("../../manuals/documentation/testDoc.xps", FileAccess.Read);
            mainDocViewer.Document = testDocument.GetFixedDocumentSequence();
        }
        #endregion
        /********************************** END OF STANDARD TOP MENU FUNCTIONS ******************************/
        /**************************************** TOOLBAR MENU FUNCTIONS ************************************/
        #region ToolBarFunctions
        /*
         * OnUndo_Clicked -  Handle event if the Undo button is 
         *                      click from the toolbar
         */
        private void OnUndo_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Undo Clicked - Fix it");
        }

        /*
         * OnRedo_Clicked -  Handle event if the Redo button is 
         *                  click from the toolbar
         */
        private void OnRedo_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Redo Clicked - Fix it");
        }

        /*
         * OnForward_Clicked -  Handle event if the Forward button is 
         *                      click from the toolbar
         */
        private void OnForward_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Forward Clicked - Fix it");
        }

        /*
         * OnBack_Clicked -  Handle event if the Back button is 
         *                  click from the toolbar
         */
        private void OnBack_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Back Clicked - Fix it");
        }

        /*
         * OnNew_Clicked -  Handle event if the New button is 
         *                  click from the toolbar
         */
        private void OnNew_Clicked(object sender, RoutedEventArgs e)
         {
            MessageBox.Show("New Clicked - Fix it");
         }

        /*
         * OnOpen_Clicked -  Handle event if the Open button is 
         *                  click from the toolbar
         */
        private void OnOpen_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open Clicked - Fix it");
        }

        /*
         * OnSave_Clicked - Handle event if the Save button is 
         *                  click from the toolbar
         */
        private void OnSave_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save Clicked - Fix it");
        }

        /*
         * OnSaveAs_Clicked - Handle event if the Save As button is 
         *                  click from the toolbar
         */
        private void OnSaveAs_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Save As Clicked - Fix it");
        }

        /*
         * OnPrint_Clicked - Handle event if the Print button is 
         *                  click from the toolbar
         */
        private void OnPrint_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Print Clicked - Fix it");
        }

        /*
         * OnPageSetup_Clicked - Handle event if the Page Setup button is 
         *                      click from the toolbar
         */
        private void OnPageSetup_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Page Setup Clicked - Fix it");
        }

        /*
         * OnCut_Clicked -  Handle event if the Cut button is 
         *                  click from the toolbar
         */
        private void OnCut_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cut Clicked - Fix it");
        }

        /*
         * OnCopy_Clicked - Handle event if the Copy button is 
         *                  click from the toolbar
         */
        private void OnCopy_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Copy Clicked - Fix it");
        }

        /*
         * OnPaste_Clicked - Handle event if the Paste button is 
         *                   click from the toolbar
         */
        private void OnPaste_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Paste Clicked - Fix it");
        }

        /*
         * OnSettings_Clicked - Handle event if the Settings button is 
         *                  click from the toolbar
         */
        private void OnSettings_Clicked(object sender, RoutedEventArgs e)
        {
            Settings appSettings = new Settings();
            appSettings.Show();
        }

        /*
         * OnHelp_Clicked - Handle event if the Help button is 
         *                   click from the toolbar
         */
        private void OnHelp_Clicked(object sender, RoutedEventArgs e)
        {
            HelpLibrary library = new HelpLibrary();
            library.Show();
        }

        /*
         * OnKeypad_Clicked - Handle event if the Keypad button is 
         *                   click from the toolbar
         */
        private void OnKeypad_Clicked(object sender, RoutedEventArgs e)
        {
            KeyPad keypad = new KeyPad();
            keypad.Show();
        }

        /*
         * OnGraph_Clicked - Handle event if the Graph button is 
         *                   click from the toolbar
         */
        private void OnGraph_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Graph Clicked - Fix it");
        }
        #endregion
        /************************************ END OF TOOLBAR MENU FUNCTIONS *********************************/
        /********************************* GREEK CHARACTERS KEYPAD FUNCTIONS ********************************/
        #region GreekCharacters 

        /*
         * onAlpha_Clicked -    Function for the Alpha Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnAlpha_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03B1";
        }

        /*
         * onBeta_Clicked - Function for the Beta Character Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnBeta_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03B2";
        }

        /*
         * onDelta_Clicked -    Function for the Delta Button Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnDelta_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u0394";
        }

        /*
         * on_delta_Clicked -   Function for the lower case delta Character Button
         *                      on the keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void On_delta_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03B4";
        }

        /*
         * onEpsilon_Clicked -  Function for the Epsilon Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnEpsilon_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03B5";
        }

        /*
         * onGamma_Clicked -    Function for the Gamma Button Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnGamma_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03B3";
        }

        /*
         * onLambda_Clicked -   Function for the Lambda Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnLambda_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03BB";
        }

        /*
         * onTheta_Clicked -    Function for the Theta Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnTheta_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03B8";
        }

        /*
         * onMu_Clicked -   Function for the Mu Button Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnMu_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03BC";
        }

        /*
         * onOmega_Clicked -    Function for the Omega Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnOmega_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03A9";
        }

        /*
         * on_omega_Clicked -   Function for the Lower case Omega Character Button
         *                      on the keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void On_omega_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03C9";
        }

        /*
         * onPhi_Clicked -  Function for the Phi Button Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnPhi_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03C6";
        }

        /*
         * onPsi_Clicked -  Function for the Psi Character Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code         */
        private void OnPsi_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03C8";
        }

        /*
         * onRho_Clicked -  Function for the Rho Character Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnRho_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03C1";
        }

        /*
         * onSigma_Clicked - Function for the Sigma Button Button on the
         *                   keypad in the right side of the Main Window
         *                   Dock Panel - NOTE: Character created with
         *                   Unicode Escape Characters/Code
         */
        private void OnSigma_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            this.inputBox.Text += "\u03A3";
        }

        #endregion
        /***************************** END OF GREEK CHARACTERS KEYPAD FUNCTIONS *****************************/
        /************************** ALGEBRA/MATHEMATICAL FUNCTIONS KEYPAD FUNCTIONS *************************/
        #region AlgebraFunctions
        /*
         * onApprox_Clicked -   Function for the Approx Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnApprox_Clicked(object sender, RoutedEventArgs e)
        {
            // NEED TO THINK ABOUT THIS
            this.inputBox.Text += "\u2248";
        }

        /*
         * onLeftBracketClicked -   Function for the Left Bracket Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnLeftBracket_Clicked(object sender, RoutedEventArgs e)
        {
            // \u0028 => "(" => Left/Opening Parenthesis
            this.inputBox.Text += "(";
        }

        /*
         * onRightBracketClicked -  Function for the Right Bracket Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnRightBracket_Clicked(object sender, RoutedEventArgs e)
        {
            // \u0029 => ")" => Right/Closing Parenthesis
            this.inputBox.Text += ")";
        }

        /*
         * OnLessEqual_Clicked -   Function for the Less Than or Equal to Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnLessEqual_Clicked(object sender, RoutedEventArgs e)
        {
            // \u0028 => "(" => Left/Opening Parenthesis
            this.inputBox.Text += "<=";
        }

        /*
         * OnMoreEqual_Clicked -  Function for the Greater than or Equal to Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnMoreEqual_Clicked(object sender, RoutedEventArgs e)
        {
            // \u0029 => ")" => Right/Closing Parenthesis
            this.inputBox.Text += ">=";
        }

        /*
         * onIndiceClicked -    Function for the Indice Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel
         */
        private void OnIndice_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "^";
        }

        /*
         * onMulClicked -   Function for the Multiplication Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnMul_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            // \u00D7 => \times => x
            // \u00B7 => \cdot => . (but in the middle of the line!)
            this.inputBox.Text += "\u00D7";
        }

        /*
         * onDivClicked -   Function for the Division Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnDiv_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            // \u00F7 => Division Sign
            // \u2215 => Division Slash => /
            this.inputBox.Text += "\u00F7";
        }

        /*
         * onFracClicked -  Function for the Fraction Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnFrac_Clicked(object sender, RoutedEventArgs e)
        {
            // Need to think about symbol since it clashes with above
            // OnDivClicked
            // \u2044 => Fraction Slash (Unicode Charatcer)
            this.inputBox.Text += "/";
        }

        /*
         * onAddClicked -   Function for the Addition Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnAdd_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Code/Characters
            // \u002B => + (Unicode Character)
            this.inputBox.Text += "+";
        }

        /*
         * onSubClicked -   Function for the Subtraction Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnSub_Clicked(object sender, RoutedEventArgs e)
        {
            // \u002D => Hypen-Minus in Unicode
            this.inputBox.Text += "-";
        }

        /*
         * onSqrtClicked -  Function for the Square Root Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnSqrt_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Characters/Code to render special Characters
            // \u221A => Square Root
            // \u221B => Cube Root
            // \u221C => Fourth Root
            this.inputBox.Text += "sqrt()";
        }

        /*
         * onPercentClicked -  Function for the Percentage Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel
         */
        private void OnPercent_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "%";
        }

        /*
         * onPiClicked -    Function for the Pi Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnPi_Clicked(object sender, RoutedEventArgs e)
        {
            // Use Unicode Escape Characters/Code to render special Characters
            this.inputBox.Text += "\u03C0";
        }

        /*
         * onN_SqrtClicked - Function for the N Square Root Button on the
         *                   keypad in the right side of the Main Window
         *                   Dock Panel - NOTE: Character can be created
         *                   with Unicode Escape Characters/Code
         */
        private void OnN_Sqrt_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "root()";
        }

        /*
         * onSinClicked -   Function for the Sin Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnSin_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "sin()";
        }

        /*
         * onCosClicked -   Function for the Cos Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnCos_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "cos()";
        }

        /*
         * onTanClicked -   Function for the Tan Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnTan_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "tan()";
        }

        #endregion
        /********************** END OF ALGEBRA/MATHEMATICAL FUNCTIONS KEYPAD FUNCTIONS **********************/
        /************************************** NUMERICAL KEYPAD FUNCTIONS **********************************/
        #region Numerical
        /*
         * onEqualClicked -    Function for the equals Button on the
         *                     keypad in the right side of the Main Window
         *                     Dock Panel - NOTE: Character can be created
         *                     with Unicode Escape Characters/Code
         */
        private void OnEqual_Clicked(object sender, RoutedEventArgs e)
        {
            // \u0028 => "(" => Left/Opening Parenthesis
            this.inputBox.Text += "=";
        }

        /*
         * onLessThanClicked -      Function for the Less than Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnLessThan_Clicked(object sender, RoutedEventArgs e)
        {
            // \u0028 => "(" => Left/Opening Parenthesis
            this.inputBox.Text += "<";
        }

        /*
         * onGreaterThanClicked -   Function for the Greater than Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnGreaterThan_Clicked(object sender, RoutedEventArgs e)
        {
            // \u0029 => ")" => Right/Closing Parenthesis
            this.inputBox.Text += ">";
        }

        /*
         * onDel_Clicked -  Function for the Delete Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void OnDel_Clicked(object sender, RoutedEventArgs e)
        {
            // Need to have a think about this one
        }

        /*
         * onDecimalClicked -   Function for the Decimal Button on the
         *                      keypad in the right side of the Main
         *                      Window Dock Panel
         */
        private void OnDecimal_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += ".";
        }

        /*
         * onAnsClicked -   Function for the Ans Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void OnAns_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "Ans";
        }

        /*
         * on0_Clicked -    Function for the 0 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On0_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "0";
        }

        /*
         * on1_Clicked -    Function for the 1 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On1_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "1";
        }

        /*
         * on2_Clicked -    Function for the 2 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On2_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "2";
        }

        /*
         * on3_Clicked -    Function for the 3 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On3_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "3";
        }

        /*
         * on4_Clicked -    Function for the 4 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On4_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "4";
        }

        /*
         * on5_Clicked -    Function for the 5 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On5_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "5";
        }

        /*
         * on6_Clicked -    Function for the 6 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On6_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "6";
        }

        /*
         * on7_Clicked -    Function for the 7 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On7_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "7";
        }

        /*
         * on8_Clicked -    Function for the 8 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On8_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "8";
        }

        /*
         * on9_Clicked -    Function for the 9 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On9_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "9";
        }
        #endregion
        /********************************** END OF NUMERICAL KEYPAD FUNCTIONS*******************************/
        /*************************************** VARIABLE TABLE FUNCTIONS***********************************/
        #region VariableTable

        public class VariableDetails
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        /// <summary>
        /// Method to load in the variables and store them into the datagrid.
        /// </summary>
        public void loadVarsIntoDataGrid()
        {
            //Get the variables and put them into a hashtable
            Hashtable vars = VariableFileHandle.getVariables();
            //Sort the hashtable as the vars should be in alphabetical order.
            SortedDictionary<string, string> sortedVars = new SortedDictionary<string, string>();
            foreach(DictionaryEntry d in vars)
            {
                string key = (string)d.Key;
                string value = (string)d.Value;
                //Don't add the variables with the '~' symbol infront as these are not user-defined variables.
                if (key.Contains("~"))
                {
                    continue;
                }
                else
                {
                    sortedVars.Add((string)d.Key,(string)d.Value);
                }
            }

            //To add something to the datagrid it must be an ObservableCollection object.
            ObservableCollection<VariableDetails> varInfoToAdd = new ObservableCollection<VariableDetails>();
            //Add the variables to the ObservableCollection object.
            foreach(var d in sortedVars)
            {
                varInfoToAdd.Add(new VariableDetails { Name = d.Key, Value = d.Value });
            }
            //Set the itemSource of the datagrid to the ObservableCollection.
            varTable.ItemsSource = varInfoToAdd;
        }
        #endregion    
        /***********************************END OF VARIABLE TABLE FUNCTIONS*********************************/
    }
}