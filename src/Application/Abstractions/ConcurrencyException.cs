namespace Hotel.src.Application.Abstractions;

public sealed class ConcurrencyException(string message, Exception innerException)
    : Exception(message, innerException) { }
