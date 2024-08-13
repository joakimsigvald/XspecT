using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows another assertions to be made on the provided string
/// </summary>
public record IsStringContinuation : IsString
{
    internal IsStringContinuation(string actual) : base(actual)
    {
    }

    /// <summary>
    /// Asserts that the string contains the given string
    /// </summary>
    [CustomAssertion]
    public ContinueWith<DoesString> Contain(string expected)
        => new DoesString(Actual).Contain(expected);

    /// <summary>
    /// Asserts that the string does not contains the given string
    /// </summary>
    [CustomAssertion]
    public ContinueWith<DoesString> NotContain(string other)
        => new DoesString(Actual).NotContain(other);
}