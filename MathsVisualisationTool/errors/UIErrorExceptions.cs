using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * File containing all the direct children of SolveItException.UIErrorException
 */
namespace MathsVisualisationTool
{

    /// <summary>
    /// Error thrown if the user tries to save an empty variable table.
    /// </summary>
    public class EmptyVarSaveException : UIErrorException
    {
        public EmptyVarSaveException(string message) : base(message)
        {
            ErrorCode = "7.0";
        }
    }

    /// <summary>
    /// Error thrown if there are any problems loading in the variable json file.
    /// </summary>
    public class ErrorLoadingVariableFileException : UIErrorException
    {
        public ErrorLoadingVariableFileException(string message) : base(message)
        {
            ErrorCode = "7.1";
        }
    }

    /// <summary>
    /// Error thrown if there are any problems loading in the numerical workshop text file.
    /// </summary>
    public class ErrorLoadingNumericalWorkshopFileException : UIErrorException
    {
        public ErrorLoadingNumericalWorkshopFileException(string message) : base(message)
        {
            ErrorCode = "7.2";
        }
    }

    /// <summary>
    /// Error thrown if the user tries to save an empty workshop.
    /// </summary>
    public class EmptyWorkshopException : UIErrorException
    {
        public EmptyWorkshopException(string message) : base(message)
        {
            ErrorCode = "7.3";
        }
    }

    /// <summary>
    /// Error thrown if the directory given in the save all function is invalid.
    /// </summary>
    public class InvalidDirectoryException : UIErrorException
    {
        public InvalidDirectoryException(string message):base(message)
        {
            ErrorCode = "7.4";
        }
    }
}
