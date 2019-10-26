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

namespace MathsVisualisationTool
{
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


        public class VariableDataGrid
        {
            public string AssignedVariable { get; set; }
            public string StoredValue { get; set; }
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
        private void OnSubmitClicked(object sender, EventArgs e)
        {
            HandleTextEnter();
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
