namespace XspecT;

/// <summary>
/// TODO
/// </summary>
public class SetupFailed : ApplicationException
{
    internal SetupFailed(string message) : base(message) { }

    internal SetupFailed(string message, Exception innerException)
        : base($"{message}, because: {innerException.Message}", innerException) { }
}