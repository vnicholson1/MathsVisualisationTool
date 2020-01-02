using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Controls;
using DataDomain;

namespace MathsVisualisationTool
{
    class LiveChartsDrawer
    {
        //Required for live charts
        public string[] Labels { get; set; }
        public ChartValues<double> Yvalues;
        //Reference to the MainWindow Controller of the program.
        public MainWindow window;
        //Data
        public List<DataPoint> dataPoints = null;
        public double[] canvasXLabels = null;
        public string Xname = null;
        public string Yname = null;

        /// <summary>
        /// Constructor for the LiveChartsDrawerObject.
        /// </summary>
        /// <param name="windowToDrawOn">A reference to the window that LiveCharts is located.</param>
        public LiveChartsDrawer(MainWindow windowToDrawOn)
        {
            window = windowToDrawOn;
        }

        /// <summary>
        /// Function called to draw the graph onto LiveCharts.
        /// </summary>
        public void Draw()
        {
            //if both null then just draw the default line
            if(dataPoints is null && canvasXLabels is null)
            {
                //Default values
                Yvalues = new ChartValues<double> { 0,1, 2, 3, 4, 5, 6, 7, 8, 9};
                Labels = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                Xname = "X";
                Yname = "Y";
            } else
            {
                Yvalues = getYValues();
                Labels = getXValues();
            }

            renderGraph();
        }

        private ChartValues<double> getYValues()
        {
            var Yvalues = new ChartValues<double>();
            int count = 0;

            foreach(DataPoint d in dataPoints)
            {
                if(count%11 == 0)
                {
                    Yvalues.Add(d.getY());
                    Console.WriteLine(d.getY());
                }
                count++;
            }

            //return new ChartValues<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9 , 10};
            return Yvalues;
        }

        private string [] getXValues()
        {
            var Xvalues = new string[canvasXLabels.Length];
            
            for(int i=0;i<canvasXLabels.Length;i++)
            {
                Xvalues[i] = Convert.ToString(canvasXLabels[i]);
            }

            //return new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9","10" };
            return Xvalues;
        }

        /// <summary>
        /// Function to render the graph given the data gathered in Draw().
        /// </summary>
        private void renderGraph()
        {
            window.Y_Axis.Title = Yname;
            window.X_Axis.Title = Xname;

            //Initially only clear the line if its already there.
            if(window.LvChrt.Series.Count != 0)
            {
                window.LvChrt.Series.Clear();
            }

            ////////////////////////////////////////////////////////////////////////////////////////////
            ///ANDY ----- DO THE STYLING OF THE GRAPHS HERE ///////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////////////////////
            window.LvChrt.Series.Add(
                new LineSeries
                {
                    Title = Yname + "=",
                    //YValues
                    Values = Yvalues,
                }
            );
            window.X_Axis.Labels = Labels;

            ///////////////////////////////////////////////////////////////////////////////////////////
        }
    }
}
