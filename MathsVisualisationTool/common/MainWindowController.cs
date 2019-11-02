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
         * OnSubmitClicked - Handle event if the Submit button is 
         *                  clicked.
         */
        private void OnSubmitClicked(object sender, RoutedEventArgs e)
        {
            //HandleTextEnter();
            if (this.inputBox.Text != " ")
            {
                Results.Items.Add(this.inputBox.Text);
                this.inputBox.Focus();
                this.inputBox.Clear();
               
            }
            else
            {
                MessageBox.Show("ERROR");
                this.inputBox.Focus();
            }       
        }

        private void OnDecimalClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnAnsClicked(object sender, RoutedEventArgs e)
        {

        }
        private void On0_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On1_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On2_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On3_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On4_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On5_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On6_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On7_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On8_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void On9_Clicked(object sender, RoutedEventArgs e)
        {

        }

        /*
         * EnterKeyPressed - Handle event if the Enter key has been 
         *                  pressed.
         */
        private void EnterKeyPressed(object sender, RoutedEventArgs e)
        {
            HandleTextEnter();
        }

        /*
         * InputBox_KeyDown - Handle event if the return button has 
         *                  been pressed.
         */ 
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                HandleTextEnter();
                e.Handled = true;
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
            } catch (Exception e)
            {
                string output = e.ToString();
                Console.WriteLine(output); //output onto the screen but in red.
            }
           
        }
    }
}
