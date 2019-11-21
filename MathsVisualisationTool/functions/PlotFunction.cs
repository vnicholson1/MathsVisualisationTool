using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace MathsVisualisationTool
{
    class PlotFunction
    {
        //plot func has the following syntax:
        //plot(algebraFunc,Xmin,Xmax,inc)
        private List<Token> algebraFunc; //This could be something Like Y=sin(x) or Y=X^2.
        private int Xmin;
        private int Xmax;
        private int inc;

        public PlotFunction(List<Token> algebraFunc,int Xmin,int Xmax,int inc)
        {
            this.algebraFunc = algebraFunc;
            this.Xmin = Xmin;
            this.Xmax = Xmax;
            this.inc = inc;
        }
    }
}
