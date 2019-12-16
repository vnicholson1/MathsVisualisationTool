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

    public partial class GraphDrawer : Window
    {

        private PlotFunction plotFunc;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //Properties of the graph
        //The Margin of the canvas.
        private readonly uint MARGIN = 60;
        //The height of the point markers on the x and y axes.
        private readonly uint HEIGHT_OF_POINT_MARKERS = 10;
        //Since we don't want the markers to reach right at the end of the axis, this defines how much
        //left-over space there is on each axis.
        private readonly uint TAIL_OF_AXIS = 8;
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //For recording the length of each axis in the canvas in terms of number of pixels.
        private uint AXIS_LENGTH;
        //Min and Max X value in catersian space.
        private double Xmin;
        private double Xmax;
        //The Min and Max X value for the canvas.
        private double xminCanvas;
        private double xmaxCanvas;
        //Min and Max Y value in cartesian space.
        private double Ymin;
        private double Ymax;
        //The Min and Max Y value for the canvas.
        private double yminCanvas;
        private double ymaxCanvas;
        //Num pixels inbetween each data point on each axes.
        private double step;
        //Arrays storing the values of each marker on their respective axes.
        private double[] Xlabels;
        private double[] Ylabels;


        public GraphDrawer(PlotFunction plotFunc)
        {
            this.plotFunc = plotFunc;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //The Canvas in the graph drawing window must be a square.
            if (graphCanvas.Height != graphCanvas.Width)
            {
                throw new Exception("Make sure height and width are the same - From Vince");
            }

            //Setting up field variables for the canvas.
            xminCanvas = MARGIN;
            xmaxCanvas = graphCanvas.Width - TAIL_OF_AXIS;
            yminCanvas = MARGIN;
            ymaxCanvas = graphCanvas.Height - TAIL_OF_AXIS;
            AXIS_LENGTH = Convert.ToUInt32(xmaxCanvas - xminCanvas);

            //Setting up field variables in terms of the catesian space.
            Xmin = plotFunc.Xmin;
            Xmax = plotFunc.Xmax;
            Ymin = plotFunc.Ymin;
            Ymax = plotFunc.Ymax;
            //Calculate the difference between each marking on the axis.
            //Calculated by the length of the axis / (number of dataPoints)
            int numMarkers = 0;
            if(plotFunc.dataPoints.Count > 10)
            {
                numMarkers = 9;
            } else
            {
                numMarkers = plotFunc.dataPoints.Count - 1;
            }

            getXandYLabels(numMarkers + 1);

            step = AXIS_LENGTH / Convert.ToDouble(numMarkers);

            createXaxis();

            createYaxis();

            drawDataPoints();
        }

        /// <summary>
        /// Method for drawing the X axis into the canvas.
        /// </summary>
        private void createXaxis()
        {
            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            //Create a straight horizontal line
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(xminCanvas, graphCanvas.Height - yminCanvas), new Point(graphCanvas.Width, graphCanvas.Height - yminCanvas)));

            int count = 0;
            //Iterate through and add | markings to this line.
            for (double x = xminCanvas;
                x <= xmaxCanvas; x += step)
            {
                DrawText(x+6, graphCanvas.Height - (yminCanvas-6), Xlabels[count].ToString(),true);
                //Add | per each step.
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, graphCanvas.Height - yminCanvas - HEIGHT_OF_POINT_MARKERS / 2),
                    new Point(x, graphCanvas.Height - yminCanvas + HEIGHT_OF_POINT_MARKERS / 2)));
                count++;
            }

            //Then style the X axis.
            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;
            //Add it to the graphCanvas.
            graphCanvas.Children.Add(xaxis_path);
        }

        /// <summary>
        /// Method for drawing the Y axis into the canvas.
        /// </summary>
        private void createYaxis()
        {
            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            //Create a vertical line 
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xminCanvas, graphCanvas.Height - yminCanvas), new Point(xminCanvas, 0)));

            int count = 0;
            //Iterate through and add | markings to this line.
            for (double y = yminCanvas; y <= ymaxCanvas; y += step)
            {
                //To work out the formatting of numbers
                string stringRep = Ylabels[count].ToString();

                DrawText((MARGIN-8) - (stringRep.Length*7), graphCanvas.Height - y - 6, stringRep, false);
                //Add | per each step
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xminCanvas - HEIGHT_OF_POINT_MARKERS / 2, graphCanvas.Height - y),
                    new Point(xminCanvas + HEIGHT_OF_POINT_MARKERS / 2, graphCanvas.Height - y)));
                count++;
            }
            //Then style the Y axis
            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;
            //Add it to the graph canvas
            graphCanvas.Children.Add(yaxis_path);
        }

        /// <summary>
        /// Method for drawing the data points onto the canvas.
        /// </summary>
        private void drawDataPoints()
        {
            // Make some data sets.
            Brush colour = Brushes.Blue;
            PointCollection points = new PointCollection();
            foreach (Tuple<double, double> point in plotFunc.dataPoints)
            {
                points.Add(new Point(convertXCoordinate(point.Item1), convertYCoordinate(point.Item2)));
            }

            Polyline polyline = new Polyline();
            polyline.StrokeThickness = 1;
            polyline.Stroke = colour;
            polyline.Points = points;

            graphCanvas.Children.Add(polyline);
        }

        /// <summary>
        /// Method to convert a given x coordinate in cartesian space to the canvas coordinate. 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double convertXCoordinate(double x)
        {
            //get the percentage that x lies inbetween Xmin and Xmax
            double proportion = (x-Xmin) / (Xmax-Xmin);

            return proportion* AXIS_LENGTH + MARGIN;
        }

        /// <summary>
        /// Method to convert a given y coordinate in cartesian space to the canvas coordinate. 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double convertYCoordinate(double y)
        {
            //get the percentage that x lies inbetween Xmin and Xmax
            double proportion = (y - Ymin) / (Ymax - Ymin);

            return (ymaxCanvas + TAIL_OF_AXIS - MARGIN) - (proportion * AXIS_LENGTH);
        }

        /// <summary>
        /// Function to add text onto the canvas.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="rotate"></param>
        private void DrawText(double x, double y, string text,bool rotate)
        {

            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;
            textBlock.FontFamily = new FontFamily("Courier New");

            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(0,0,0));

            Canvas.SetLeft(textBlock, x);

            Canvas.SetTop(textBlock, y);

            if (rotate)
            {
                textBlock.RenderTransform = new RotateTransform(90);
            }

            graphCanvas.Children.Add(textBlock);
        }

        /// <summary>
        /// Function for collecting the numbers marked on the X and Y axis.
        /// </summary>
        /// <param name="numMarkers">the number of markers on both axis.</param>
        private void getXandYLabels(int numMarkers)
        {
            Xlabels = new double[numMarkers];
            Ylabels = new double[numMarkers];

            double Xinc = (Xmax - Xmin + 1) / numMarkers;
            double Yinc = (Ymax - Ymin + 1) / numMarkers;

            int i;
            for(i=0;i<numMarkers;i++)
            {
                Xlabels[i] = Xmin + i * Xinc;
            }

            for (i = 0; i < numMarkers; i++)
            {
                Ylabels[i] = Ymin + i * Yinc;
            }

        }
    }
}
