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
        private readonly uint MARGIN = 10;

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

        public GraphDrawer(PlotFunction plotFunc)
        {
            this.plotFunc = plotFunc;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Setting up field variables in terms of the canvas.
            xminCanvas = MARGIN;
            xmaxCanvas = graphCanvas.Width - MARGIN;
            yminCanvas = MARGIN;
            ymaxCanvas = graphCanvas.Height - MARGIN;
            //Setting up field variables interms of the catesian space.
            Xmin = plotFunc.Xmin;
            Xmax = plotFunc.Xmax;
            Ymin = plotFunc.Ymin;
            Ymax = plotFunc.Ymax;
            step = Math.Floor(480.0/ Convert.ToDouble(plotFunc.dataPoints.Count - 1));

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
            //Create a straight horizontal line from (0,ymaxCanvas) to (canvas.width,ymaxCanvas) in canvas coordinates.
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymaxCanvas), new Point(graphCanvas.Width, ymaxCanvas)));

            //Iterate through and add | markings to this line.
            for (double x = xminCanvas;
                x <= graphCanvas.Width; x += step)
            {
                //Add | per each step.
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymaxCanvas - MARGIN / 2),
                    new Point(x, ymaxCanvas + MARGIN / 2)));
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
            //Create a vertical line starting from (xminCanvas,0) to (xminCanvas, canvas.height)
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xminCanvas, 0), new Point(xminCanvas, graphCanvas.Height)));

            //Iterate through and add | markings to this line.
            for (double y = MARGIN; y <= graphCanvas.Height; y += step)
            {
                //Add | per each step
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xminCanvas - MARGIN / 2, graphCanvas.Height - y),
                    new Point(xminCanvas + MARGIN / 2, graphCanvas.Height - y)));
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
        public double convertXCoordinate(double x)
        {
            //get the percentage that x lies inbetween Xmin and Xmax
            double proportion = (x-Xmin) / (Xmax-Xmin);

            double canvasWidth = xmaxCanvas - xminCanvas;

            return proportion*canvasWidth + MARGIN;
        }

        /// <summary>
        /// Method to convert a given y coordinate in cartesian space to the canvas coordinate. 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double convertYCoordinate(double y)
        {
            //get the percentage that x lies inbetween Xmin and Xmax
            double proportion = (y - Ymin) / (Ymax - Ymin);

            double canvasHeight = ymaxCanvas - yminCanvas;

            return ymaxCanvas - (proportion * canvasHeight);
        }
    }
}
