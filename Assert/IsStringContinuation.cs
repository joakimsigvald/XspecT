namespace XspecT.Assert;

/// <summary>
/// Object that allows another assertions to be made on the provided string
/// </summary>
public class IsStringContinuation : IsString
{
    internal IsStringContinuation(string actual) : base(actual)
    {
    }

    /// <summary>
    /// Asserts that the string contains the given string
    /// </summary>
    public ContinueWith<DoesString> Contain(string expected)
        => new DoesString(_actual).Contain(expected);

    /// <summary>
    /// Asserts that the string does not contains the given string
    /// </summary>
    public ContinueWith<DoesString> NotContain(string other)
        => new DoesString(_actual).NotContain(other);
}