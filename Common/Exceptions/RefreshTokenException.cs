using System;


namespace Common.Exceptions
{
    public class RefreshTokenException
        : Exception
    {
        public RefreshTokenException(string message)
            : base(message)
        {

        }
    }
}
