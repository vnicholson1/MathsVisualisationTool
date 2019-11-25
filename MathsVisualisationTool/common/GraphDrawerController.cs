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
        private readonly int MARGIN = 10;

        public GraphDrawer(PlotFunction plotFunc)
        {
            this.plotFunc = plotFunc;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //margin of the canvas
            double xminCanvas = MARGIN;
            //Width of the canvas minus desired margin of canvas.
            double xmaxCanvas = graphCanvas.Width - MARGIN;
            double yminCanvas = MARGIN;
            double ymaxCanvas = graphCanvas.Height - MARGIN;
            //defines the number of pixels between each data point.
            double step = Math.Floor(480.0/ Convert.ToDouble(plotFunc.dataPoints.Count - 1));

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
                    new Point(xminCanvas - MARGIN / 2, y),
                    new Point(xminCanvas + MARGIN / 2, y)));
            }
            //Then style the Y axis
            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;
            //Add it to the graph canvas
            graphCanvas.Children.Add(yaxis_path);

            // Make some data sets.
            Brush colour = Brushes.Blue;
            PointCollection points = new PointCollection();
            foreach (Tuple<double,double> point in plotFunc.dataPoints)
            {
                points.Add(new Point(point.Item1*step + MARGIN, point.Item2*200));
            }

            Polyline polyline = new Polyline();
            polyline.StrokeThickness = 1;
            polyline.Stroke = colour;
            polyline.Points = points;

            graphCanvas.Children.Add(polyline);
        }
    }
}
