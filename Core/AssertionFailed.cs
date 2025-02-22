using Xunit.Sdk;

namespace XspecT;

/// <summary>
/// Exception thrown when an assertion is violated
/// </summary>
public class AssertionFailed(string message, XunitException? innerException = null)
    : XunitException(message, innerException)
{ }