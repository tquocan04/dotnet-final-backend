﻿namespace ExceptionHandler
{
    public abstract class BadRequestException(string message) : Exception(message)
    {
    }
}
