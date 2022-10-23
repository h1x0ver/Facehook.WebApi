using System;

namespace Facehook.Exceptions.FileExceptions

{
    public class FileException : Exception
    {
        public FileException(string message) : base(message) { }
    }
}
