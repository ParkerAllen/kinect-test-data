using System;

[Serializable]
public class InvalidExtensionException : Exception
{
    public InvalidExtensionException() : base() { }
    public InvalidExtensionException(string message) : base(message) { }
    public InvalidExtensionException(string message, Exception inner) : base(message, inner) { }
}
