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

        public GraphDrawer(PlotFunction plotFunc)
        {
            this.plotFunc = plotFunc;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //margin of the canvas
            const double margin = 10; 
            double xminCanvas = margin;
            //Width of the canvas minus desired margin of canvas.
            double xmaxCanvas = graphCanvas.Width - margin;
            double yminCanvas = margin;
            double ymaxCanvas = graphCanvas.Height - margin;

            //defines the number of pixels between each data point.
            const double step = 10;

            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(
                new Point(0, ymaxCanvas), new Point(graphCanvas.Width, ymaxCanvas)));
            for (double x = xminCanvas + step;
                x <= graphCanvas.Width - step; x += step)
            {
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x, ymaxCanvas - margin / 2),
                    new Point(x, ymaxCanvas + margin / 2)));
            }

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            graphCanvas.Children.Add(xaxis_path);

            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(
                new Point(xminCanvas, 0), new Point(xminCanvas, graphCanvas.Height)));
            for (double y = step; y <= graphCanvas.Height - step; y += step)
            {
                yaxis_geom.Children.Add(new LineGeometry(
                    new Point(xminCanvas - margin / 2, y),
                    new Point(xminCanvas + margin / 2, y)));
            }

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            graphCanvas.Children.Add(yaxis_path);

            // Make some data sets.
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue };
            Random rand = new Random();
            for (int data_set = 0; data_set < 3; data_set++)
            {
                int last_y = rand.Next((int)yminCanvas, (int)ymaxCanvas);

                PointCollection points = new PointCollection();
                for (double x = xminCanvas; x <= xmaxCanvas; x += step)
                {
                    last_y = rand.Next(last_y - 10, last_y + 10);
                    if (last_y < yminCanvas) last_y = (int)yminCanvas;
                    if (last_y > ymaxCanvas) last_y = (int)ymaxCanvas;
                    points.Add(new Point(x, last_y));
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                graphCanvas.Children.Add(polyline);
            }
        }
    }
}
