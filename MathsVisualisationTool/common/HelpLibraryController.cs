using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;

namespace MathsVisualisationTool
{
    public partial class HelpLibrary : Window
    {

        public HelpLibrary()
        {
            InitializeComponent();
        }

        /*
         * OnInputDoc_Clicked -  
         */
        private void OnInputDoc_Clicked(object sender, RoutedEventArgs e)
        {
            XpsDocument testDocument = new XpsDocument("../../manuals/documentation/testDoc.xps", FileAccess.Read);
            libraryDocViewer.Document = testDocument.GetFixedDocumentSequence();
        }
    }
}