﻿namespace Lagalt_Backend.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id) : base($"User with id {id} was not found")
        {

        }
    }
}
