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
    /// Interaction logic for MathsVisualizationToolHome.xaml
    /// </summary>
    public partial class MathsVisualizationToolHome : Page
    {
        public MathsVisualizationToolHome()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MathsVisualisationToolReportPage reportPage = new MathsVisualisationToolReportPage(this.peopleListBox.SelectedItem);
            NavigationService.Navigate(reportPage);
        }
    }
}
