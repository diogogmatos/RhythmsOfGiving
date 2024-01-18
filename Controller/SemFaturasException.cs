namespace RhythmsOfGiving.Controller;

public class SemFaturasException: Exception
{
    public SemFaturasException() { }
    public SemFaturasException(string message) : base(message) { }

    public SemFaturasException(string message, Exception innerException)
        : base(message, innerException) { }
    
}