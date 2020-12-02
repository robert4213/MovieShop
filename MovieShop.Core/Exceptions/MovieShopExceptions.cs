using System;

namespace MovieShop.Core.Exceptions
{
    public class MovieShopExceptions
    {
        
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string type, int id):base($"No {type} found, id is {id}.")
        {
            
        }
    }
    
    public class ConflictException : Exception
    {
        public ConflictException(string message):base(message)
        {
            
        }
    }
}