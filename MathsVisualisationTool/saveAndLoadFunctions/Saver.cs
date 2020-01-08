using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MathsVisualisationTool
{
    public static class Saver
    {
        /// <summary>
        /// Function used to save the variables into an external file.
        /// </summary>
        public static void saveVariablesIntoExternalFile()
        {
            Hashtable vars = VariableFileHandle.getVariables();
            //if the variable table is empty then throw an error.
            //NOTE: Not 0 with ~a - FIX LATER........
            if (vars.Count == 0)
            {
                EmptyVarSaveException err = new EmptyVarSaveException("Variables Table is Empty.");
                ErrorMsg eMsg = new ErrorMsg(err.Message, err.ErrorCode);
                eMsg.ShowDialog();
                return;
            }

            //Save the variables
            SaveFileDialog saveVar = new SaveFileDialog();
            saveVar.Filter = "JSON file(*.json)|*.json";
            saveVar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveVar.ShowDialog() == true)
            {
                TextWriter TW = new StreamWriter(saveVar.FileName);
                int counter = 0;
                int total = vars.Count;

                TW.WriteLine("[");
                foreach (DictionaryEntry pair in vars)
                {
                    string key = (string)pair.Key;
                    string value = (string)pair.Value;
                    if (key.Contains("~"))
                    {
                        total--;
                        continue;
                    }

                    TW.WriteLine("\t{");
                    TW.WriteLine("\t\t\"name\": " + "\"" + key + "\",");
                    TW.WriteLine("\t\t\"value\": " + "\"" + value + "\"");

                    if (counter == (total - 1))
                    {
                        TW.WriteLine("\t}");
                    }
                    else
                    {
                        TW.WriteLine("\t},");
                    }
                    counter++;
                }
                TW.WriteLine("]");
                TW.Close();
            }
        }

        /// <summary>
        /// Function used to save the contents of the live charts onto an external PNG file.
        /// </summary>
        public static void saveLiveChartsToExternalFile(MainWindow w)
        {
            SaveFileDialog saveLVC = new SaveFileDialog();
            saveLVC.Filter = "PNG file(*.png)|*.png";
            saveLVC.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveLVC.ShowDialog() == true)
            {
                Rect bounds = VisualTreeHelper.GetDescendantBounds(w.LvChrt);
                double dpi = 96d;
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(w.LvChrt);
                    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }
                rtb.Render(dv);
                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                pngEncoder.Save(memStream);
                memStream.Close();
                System.IO.File.WriteAllBytes(saveLVC.FileName, memStream.ToArray());
            }
        }

        /// <summary>
        /// Function used to save the contents of the canvas graph onto an external PNG file.
        /// </summary>
        public static void saveCanvasGraphOntoExternalFile(MainWindow w)
        {
            SaveFileDialog saveCanvas = new SaveFileDialog();
            saveCanvas.Filter = "PNG file(*.png)|*.png";
            saveCanvas.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveCanvas.ShowDialog() == true)
            {
                Rect bounds = VisualTreeHelper.GetDescendantBounds(w.graphCanvas);
                double dpi = 96d;
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    VisualBrush vb = new VisualBrush(w.graphCanvas);
                    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
                }
                rtb.Render(dv);
                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(rtb));

                System.IO.MemoryStream memStream = new System.IO.MemoryStream();
                pngEncoder.Save(memStream);
                memStream.Close();
                System.IO.File.WriteAllBytes(saveCanvas.FileName, memStream.ToArray());
            }
        }
    }
}
