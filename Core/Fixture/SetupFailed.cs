namespace XspecT.Fixture;

public class SetupFailed : ApplicationException
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="message"></param>
    public SetupFailed(string message) : base(message) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public SetupFailed(string message, Exception innerException)
        : base($"{message}, because: {innerException.Message}", innerException) { }
}