using System;
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

namespace MathsVisualisationTool
{
    public class VariableDataGrid
    {
        public string AssignedVariable { get; set; }
        public string StoredValue { get; set; }
    }

    public partial class MainWindow : Window
    {

        // <summary>
        // Interaction logic for MainWindow.xaml
        // </summary>
        public MainWindow()
        {
            InitializeComponent();
            inputBox.KeyDown += new KeyEventHandler(InputBox_KeyDown);
        }


        /****************************************************************************************************/

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        /****************************************************************************************************/

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

        /****************************************************************************************************/

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
                Interpreter i = new Interpreter();
                //string output = i.RunInterpreter(inputBox.Text);

                try
                {
                    Results.Items.Add(i.RunInterpreter(inputBox.Text));
                } catch(Exception exp)
                {
                    Results.Items.Add(exp.Message);
                }
                
                /**************************************************************************************/
                //Look at putting NaN "checker" here
                //if (output != "NaN")
                //{
                //    Results.Items.Add("\t\t\t Ans = " + i.RunInterpreter(inputBox.Text));
                //}
                //else
                //{
                //    MessageBox.Show("Error");
                //}
                /**************************************************************************************/
                this.inputBox.Focus();
                this.inputBox.Clear();
            }
            else
            {
                MessageBox.Show("ERROR");
                this.inputBox.Focus();
            }
        }


        /*
         * EnterKeyPressed - Handle event if the Enter key has been 
         *                  pressed.
         */
        //private void EnterKeyPressed(object sender, RoutedEventArgs e)
        //{
        //    HandleTextEnter();
        //}

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
                    Interpreter i = new Interpreter();
                    try
                    {
                        Results.Items.Add(i.RunInterpreter(inputBox.Text));
                    }
                    catch (Exception exp)
                    {
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

            Interpreter interp = new Interpreter();

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

        /****************************************************************************************************/
        /************************************** STANDARD TOP MENU FUNCTIONS *********************************/

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
            XpsDocument testDocument = new XpsDocument("../../documentation/testDoc.xps", FileAccess.Read);
            documentViewer.Document = testDocument.GetFixedDocumentSequence();
        }

        /********************************** END OF STANDARD TOP MENU FUNCTIONS ******************************/

        /**************************************** TOOLBAR MENU FUNCTIONS ************************************/

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
            MessageBox.Show("Settings Clicked - Fix it");
        }

        /*
         * OnHelp_Clicked - Handle event if the Help button is 
         *                   click from the toolbar
         */
        private void OnHelp_Clicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Help Clicked - Fix it");
        }

        /************************************ END OF TOOLBAR MENU FUNCTIONS *********************************/

        /********************************* GREEK CHARACTERS KEYPAD FUNCTIONS ********************************/

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
        /***************************** END OF GREEK CHARACTERS KEYPAD FUNCTIONS *****************************/

        /************************** ALGEBRA/MATHEMATICAL FUNCTIONS KEYPAD FUNCTIONS *************************/
        /*
         * onEquivilantClicked -   Function for the Equiviliant Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnEquivilant_Clicked(object sender, RoutedEventArgs e)
        {
            // NEED TO THINK ABOUT THIS
            //this.inputBox.Text += "(";
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
            this.inputBox.Text += "\u221A";
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
            this.inputBox.Text += "{}\u221A{}";
        }

        /*
         * onSinClicked -   Function for the Sin Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnSin_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "/sin";
        }

        /*
         * onCosClicked -   Function for the Cos Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnCos_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "/cos";
        }

        /*
         * onTanClicked -   Function for the Tan Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnTan_Clicked(object sender, RoutedEventArgs e)
        {
            this.inputBox.Text += "/tan";
        }
        /********************** END OF ALGEBRA/MATHEMATICAL FUNCTIONS KEYPAD FUNCTIONS **********************/

        /************************************** NUMERICAL KEYPAD FUNCTIONS **********************************/
        /*
         * onEqualClicked -    Function for the equals Button on the
    *                          keypad in the right side of the Main Window
    *                          Dock Panel - NOTE: Character can be created
    *                          with Unicode Escape Characters/Code
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

        /********************************** END OF NUMERICAL KEYPAD FUNCTIONS*******************************/
    }
}