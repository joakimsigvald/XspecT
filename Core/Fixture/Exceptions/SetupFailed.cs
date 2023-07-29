namespace XspecT.Fixture.Exceptions;

public class SetupFailed : ApplicationException
{
    public SetupFailed(string message) : base(message) { }
}