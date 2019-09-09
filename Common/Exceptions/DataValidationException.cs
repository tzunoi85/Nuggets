using System;


namespace Common.Exceptions
{
    public class DataValidationException
         : Exception
    {
        public object Errors { get; }

        public DataValidationException(object errors)
        {
            Errors = errors;
        }

    }
}
