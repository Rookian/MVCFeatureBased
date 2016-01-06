using System;

namespace MVCFeatureBased.Web.Infrastructure
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message)
        {
            
        }
    }
}