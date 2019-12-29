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
using System.Windows.Shapes;

namespace MathsVisualisationTool
{
    /// <summary>
    /// Interaction logic for KeyPad.xaml
    /// </summary>
    public partial class KeyPad : Window
    {
        public KeyPad()
        {
            InitializeComponent();
        }

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
            App.homeWindow.InputBoxValue += "\u03B1";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03B1";
        }

        /*
         * onBeta_Clicked - Function for the Beta Character Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnBeta_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03B2";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03B2";
        }

        /*
         * onDelta_Clicked -    Function for the Delta Button Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnDelta_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u0394";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u0394";
        }

        /*
         * on_delta_Clicked -   Function for the lower case delta Character Button
         *                      on the keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void On_delta_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03B4";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03B4";
        }

        /*
         * onEpsilon_Clicked -  Function for the Epsilon Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnEpsilon_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03B5";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03B5";
        }

        /*
         * onGamma_Clicked -    Function for the Gamma Button Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnGamma_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03B3";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03B3";
        }

        /*
         * onLambda_Clicked -   Function for the Lambda Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnLambda_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03BB";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03BB";
        }

        /*
         * onTheta_Clicked -    Function for the Theta Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnTheta_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03B8";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03B8";
        }

        /*
         * onMu_Clicked -   Function for the Mu Button Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnMu_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03BC";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03BC";
        }

        /*
         * onOmega_Clicked -    Function for the Omega Character Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void OnOmega_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03A9";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03A9";
        }

        /*
         * on_omega_Clicked -   Function for the Lower case Omega Character Button
         *                      on the keypad in the right side of the Main Window
         *                      Dock Panel - NOTE: Character created with
         *                      Unicode Escape Characters/Code
         */
        private void On_omega_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03C9";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03C9";
        }

        /*
         * onPhi_Clicked -  Function for the Phi Button Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnPhi_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03C6";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03C6";
        }

        /*
         * onPsi_Clicked -  Function for the Psi Character Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code         */
        private void OnPsi_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03C8";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03C8";
        }

        /*
         * onRho_Clicked -  Function for the Rho Character Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnRho_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03C1";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03C1";
        }

        /*
         * onSigma_Clicked - Function for the Sigma Button Button on the
         *                   keypad in the right side of the Main Window
         *                   Dock Panel - NOTE: Character created with
         *                   Unicode Escape Characters/Code
         */
        private void OnSigma_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03A3";
            // Use Unicode Escape Code/Characters
            //this.inputBox.Text += "\u03A3";
        }
        #endregion
        /***************************** END OF GREEK CHARACTERS KEYPAD FUNCTIONS *****************************/
        /************************** ALGEBRA/MATHEMATICAL FUNCTIONS KEYPAD FUNCTIONS *************************/
        #region AlgebraFunctions
        /*
         * onEquivilantClicked -   Function for the Equiviliant Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnApprox_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u2248";
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
            App.homeWindow.InputBoxValue += "(";
            // \u0028 => "(" => Left/Opening Parenthesis
            //this.inputBox.Text += "(";
        }

        /*
         * onRightBracketClicked -  Function for the Right Bracket Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnRightBracket_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += ")";
            // \u0029 => ")" => Right/Closing Parenthesis
            //this.inputBox.Text += ")";
        }

        /*
         * onIndiceClicked -    Function for the Indice Button on the
         *                      keypad in the right side of the Main Window
         *                      Dock Panel
         */
        private void OnIndice_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "^";
            //this.inputBox.Text += "^";
        }

        /*
         * onMulClicked -   Function for the Multiplication Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnMul_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u00D7";
            // Use Unicode Escape Code/Characters
            // \u00D7 => \times => x
            // \u00B7 => \cdot => . (but in the middle of the line!)
            //this.inputBox.Text += "\u00D7";
        }

        /*
         * onDivClicked -   Function for the Division Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character created with
         *                  Unicode Escape Characters/Code
         */
        private void OnDiv_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u00F7";
            // Use Unicode Escape Code/Characters
            // \u00F7 => Division Sign
            // \u2215 => Division Slash => /
            //this.inputBox.Text += "\u00F7";
        }

        /*
         * onAddClicked -   Function for the Addition Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnAdd_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "+";
            // Use Unicode Escape Code/Characters
            // \u002B => + (Unicode Character)
            //this.inputBox.Text += "+";
        }

        /*
         * onSubClicked -   Function for the Subtraction Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnSub_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "-";
            // \u002D => Hypen-Minus in Unicode
            //this.inputBox.Text += "-";
        }

        /*
         * onSqrtClicked -  Function for the Square Root Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnSqrt_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "sqrt()";
            // Use Unicode Escape Characters/Code to render special Characters
            // \u221A => Square Root
            // \u221B => Cube Root
            // \u221C => Fourth Root
            //this.inputBox.Text += "\u221A";
        }

        /*
         * onPercentClicked -  Function for the Percentage Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel
         */
        private void OnPercent_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "%";
            //this.inputBox.Text += "%";
        }

        /*
         * onPiClicked -    Function for the Pi Button on the
         *                  keypad in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnPi_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "\u03C0";
            // Use Unicode Escape Characters/Code to render special Characters
            //this.inputBox.Text += "\u03C0";
        }

        /*
         * onN_SqrtClicked - Function for the N Square Root Button on the
         *                   keypad in the right side of the Main Window
         *                   Dock Panel - NOTE: Character can be created
         *                   with Unicode Escape Characters/Code
         */
        private void OnN_Sqrt_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "root()";
            //this.inputBox.Text += "{}\u221A{}";
        }

        /*
         * onSinClicked -   Function for the Sin Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnSin_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "sin()";
            //this.inputBox.Text += "/sin";
        }

        /*
         * onCosClicked -   Function for the Cos Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnCos_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "cos()";
            //this.inputBox.Text += "/cos";
        }

        /*
         * onTanClicked -   Function for the Tan Button on the keypad
         *                  in the right side of the Main Window
         *                  Dock Panel - NOTE: Character can be created
         *                  with Unicode Escape Characters/Code
         */
        private void OnTan_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "tan()";
            //this.inputBox.Text += "/tan";
        }
        #endregion
        /********************** END OF ALGEBRA/MATHEMATICAL FUNCTIONS KEYPAD FUNCTIONS **********************/
        /************************************** NUMERICAL KEYPAD FUNCTIONS **********************************/
        #region Numerical
        /*
         * onEqualClicked -    Function for the equals Button on the
    *                          keypad in the right side of the Main Window
    *                          Dock Panel - NOTE: Character can be created
    *                          with Unicode Escape Characters/Code
         */
        private void OnEqual_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "=";
            // \u0028 => "(" => Left/Opening Parenthesis
            //this.inputBox.Text += "=";
        }

        /*
         * onLessThanClicked -      Function for the Less than Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnLessThan_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "<";
            // \u0028 => "(" => Left/Opening Parenthesis
            //this.inputBox.Text += "<";
        }

        /*
         * onGreaterThanClicked -   Function for the Greater than Button on the
         *                          keypad in the right side of the Main Window
         *                          Dock Panel - NOTE: Character can be created
         *                          with Unicode Escape Characters/Code
         */
        private void OnGreaterThan_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += ">";
            // \u0029 => ")" => Right/Closing Parenthesis
            //this.inputBox.Text += ">";
        }

        /*
         * onDecimalClicked -   Function for the Decimal Button on the
         *                      keypad in the right side of the Main
         *                      Window Dock Panel
         */
        private void OnDecimal_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += ".";
            //this.inputBox.Text += ".";
        }

        /*
         * onAnsClicked -   Function for the Ans Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void OnAns_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "Ans";
            //this.inputBox.Text += "Ans";
        }

        /*
         * on0_Clicked -    Function for the 0 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On0_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "0";
            //this.inputBox.Text += "0";
        }

        /*
         * on1_Clicked -    Function for the 1 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On1_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "1";
            //this.inputBox.Text += "1";
        }

        /*
         * on2_Clicked -    Function for the 2 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On2_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "2";
            //this.inputBox.Text += "2";
        }

        /*
         * on3_Clicked -    Function for the 3 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On3_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "3";
            //this.inputBox.Text += "3";
        }

        /*
         * on4_Clicked -    Function for the 4 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On4_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "4";
            //this.inputBox.Text += "4";
        }

        /*
         * on5_Clicked -    Function for the 5 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On5_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "5";
            //this.inputBox.Text += "5";
        }

        /*
         * on6_Clicked -    Function for the 6 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On6_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "6";
            //this.inputBox.Text += "6";
        }

        /*
         * on7_Clicked -    Function for the 7 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On7_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "7";
            //this.inputBox.Text += "7";
        }

        /*
         * on8_Clicked -    Function for the 8 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On8_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "8";
            //this.inputBox.Text += "8";
        }

        /*
         * on9_Clicked -    Function for the 9 Button on the
         *                  keypad in the right side of the Main
         *                  Window Dock Panel
         */
        private void On9_Clicked(object sender, RoutedEventArgs e)
        {
            App.homeWindow.InputBoxValue += "9";
            //this.inputBox.Text += "9";
        }
        #endregion
        /********************************** END OF NUMERICAL KEYPAD FUNCTIONS*******************************/
    }
}
