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
            typingBox.KeyDown += new KeyEventHandler(typingBox_KeyDown);
        }

        /*
         * Handle event if the enter button has been pressed.
         */
        private void enterButtonPressed(object sender, RoutedEventArgs e)
        {
            handleTextEnter();
        }

        /*
         * Handle event if the return button has been pressed.
         */ 
        private void typingBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                handleTextEnter();
                e.Handled = true;
            }
        }

        /*
         * handle the text enter and put the contents of the text box into the interpreter.
         */ 
        private void handleTextEnter()
        {
            string content = typingBox.Text;
            typingBox.Clear();

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
