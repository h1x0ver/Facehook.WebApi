using System;
using System.Collections.Generic;
using System.Text;

namespace Facehook.Business.Exceptions.DateTimeExceptions
{
    public class OneStoryPerDayException : DateException
    {
        public OneStoryPerDayException(string message) : base(message)
        {
        }
    }
}
