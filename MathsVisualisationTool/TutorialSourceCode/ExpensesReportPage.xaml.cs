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
    /// <summary>
    /// Interaction logic for MathsVisualizationToolReportPage.xaml
    /// </summary>
    public partial class MathsVisualisationToolReportPage : Page
    {
        public MathsVisualisationToolReportPage()
        {
            InitializeComponent();
        }

        //Custom constructor to pass expense report data
        public MathsVisualisationToolReportPage(object data):this()
        {
            //Bind the report data onto this object
            this.DataContext = data;
        }
    }
}
