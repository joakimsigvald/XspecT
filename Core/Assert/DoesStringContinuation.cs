namespace XspecT.Assert;

/// <summary>
/// Object that allows another assertions to be made on the provided string
/// </summary>
public record DoesStringContinuation : DoesString
{
    internal DoesStringContinuation(string actual) : base(actual)
    {
    }

    /// <summary>
    /// Continuation to assert that the string is satisfying some expectation
    /// </summary>
    /// <returns></returns>
    public IsString Is() => Actual.Is();
}