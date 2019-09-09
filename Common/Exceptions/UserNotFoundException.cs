using System;


namespace Common.Exceptions
{
    public class UserNotFoundException
         : Exception
    {
        public UserNotFoundException(string username)
            : base($"User {username} does not exists or the password is wrong!")
        {
        }
    }
}
