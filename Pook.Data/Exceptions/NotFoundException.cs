using System;

namespace Pook.Data.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message)
        {
        }
    }
}
