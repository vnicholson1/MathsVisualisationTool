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
        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string cls, string win);

        [DllImport("user32")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mutex = new Mutex(true, "MathsVisualisationTool", out bool isNewInstance);
            if (!isNewInstance)
            {
                //MessageBox.Show("Error-001A: Application is currently running!");
                ActivateWindow();
                Shutdown();
            }
        }

        private static void ActivateWindow()
        {
            var otherWindow = FindWindow(null, "Single Instance Demo");
            if (otherWindow != IntPtr.Zero)
            {
                SetForegroundWindow(otherWindow);
            }
        }
    }   
}

