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
        public NotFoundException(string type, string name, string value):base($"No {type} found, {name} is {value}.")
        {
            
        }
        public NotFoundException(string message):base(message)
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