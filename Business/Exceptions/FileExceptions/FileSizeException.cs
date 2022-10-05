using Facehook.Business.Exceptions.FileExceptions;

namespace Facehook.Business.Exceptions.FileExceptions
{
    public class FileSizeException : FileException
    {
        public FileSizeException(string message) : base(message) { }
    }
}
