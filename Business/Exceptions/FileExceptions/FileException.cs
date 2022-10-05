using System;

namespace Facehook.Business.Exceptions.FileExceptions
{
    public class FileException : Exception
    {
        public FileException(string message) : base(message) { }
    }
}
