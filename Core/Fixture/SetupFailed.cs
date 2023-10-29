namespace XspecT.Fixture;

public class SetupFailed : ApplicationException
{
    public SetupFailed(string message) : base(message) { }
    public SetupFailed(string message, Exception innerException)
        : base($"{message}, because: {innerException.Message}", innerException) { }
}