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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mutex = new Mutex(true, "MathsVisualisationTool", out bool isNewInstance);
            if (!isNewInstance)
            {
                MessageBox.Show("Error-001A: Application is currently running!");
                Shutdown();
            }
        }
    }   
}

