using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class NumRounder
    {
        /// <summary>
        /// Function used to round a double to X sig figures because C# doesn't have this built in.
        /// This funcion also converts a number to standard form if it is longer than X digits.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string RoundToSignificantDigits(double d, int digits)
        {
            if (d == 0)
            {
                return "0";
            }

            //convert the number into standard form if its absolute value is really big.
            string stringRep = Convert.ToString(Math.Round(d));
            if (stringRep.Length > digits)
            {
                string stdForm = stringRep[0] + "." + stringRep[1] + "E" + (stringRep.Length - 1);
                return stdForm;
            }
            //Do the same for really small numbers.
            if (d < 1.0)
            {
                d = Math.Round(d,digits);
                return Convert.ToString(d);
            }

            //If converting to standard form is not required, then round it.
            return convertToXDecimalPlaces(d,digits);
        }

        /// <summary>
        /// Private method to count the number of zeros until a significant digit. 
        /// E.g. 0.000004 would produce a result of 6.
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static int CountNumberOfLeadingZeros(double d)
        {
            string strRep = Convert.ToString(d);
            int count = 0;

            foreach(char c in strRep)
            {
                if(c == '0')
                {
                    count++;
                } else if(c == '.')
                {
                    continue;
                } else
                {
                    break;
                }
            }
            return count;
        }

        /// <summary>
        /// Method to round a number to X significant digits.
        /// This solution was taken from https://stackoverflow.com/questions/374316/round-a-double-to-x-significant-figures.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        private static string convertToXDecimalPlaces(double d, int digits)
        {
            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d))) + 1);
            string res = Convert.ToString(scale * Math.Round(d / scale, digits));
            return res;
        }
    }
}
