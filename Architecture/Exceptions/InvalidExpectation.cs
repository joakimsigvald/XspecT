namespace XspecT.Architecture.Exceptions;

/// <summary>
/// TODO
/// </summary>
public class InvalidExpectation : ApplicationException
{
    internal InvalidExpectation(string message) : base(message) { }
}