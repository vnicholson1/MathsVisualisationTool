using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MathsVisualisationTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        /*
         * Single Instance - If program is running in the background then the 
         *                  following code prevents multiple instances and brings
         *                  the application back into the foreground
         */
        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string cls, string win);
        [DllImport("user32")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        private static void ActivateWindow()
        {
            var otherWindow = FindWindow(null, "Single Instance Demo");
            if (otherWindow != IntPtr.Zero)
            {
                SetForegroundWindow(otherWindow);
            }
        }

        /*
         * On Start Up Function - Prevents multiple instances of the application from 
         *                      opening if the application is already running
         */
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.Show();

            var keyPad = new KeyPad { Owner = mainWindow };
            keyPad.Show();

            var mutex = new Mutex(true, "MathsVisualisationTool", out bool isNewInstance);
            if (!isNewInstance)
            {
                MessageBox.Show("Error-001A: Application is currently running!");
                ActivateWindow();
                Shutdown();
            }
        }
    }
}