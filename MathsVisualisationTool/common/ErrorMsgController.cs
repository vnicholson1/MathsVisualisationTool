using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MathsVisualisationTool
{
    public partial class ErrorMsg
    {


        public ErrorMsg(string errorMessage,string code)
        {
            InitializeComponent();
            errorCode.Text = code;
            errorMsgText.Text = errorMessage;
        }

    }
}
