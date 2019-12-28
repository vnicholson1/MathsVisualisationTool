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
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        //Reference to the MainWindow Controller of the program.
        public MainWindow window;
        //Data
        public List<DataPoint> dataPoints = null;
        public double[] canvasXLabels = null;

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
                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Y=",
                        Values = new ChartValues<double> {1,2,3,4,5,6,7,8,9,10},
                    }
                };

                //X labels
                Labels = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

                window.Y_Axis.Title = "Test - Y";
                window.X_Axis.Title = "Test - X";
            }
        }
    }
}
