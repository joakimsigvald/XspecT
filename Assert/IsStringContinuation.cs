namespace XspecT.Assert;

/// <summary>
/// TODO
/// </summary>
public class IsStringContinuation : IsString
{
    internal IsStringContinuation(string actual) : base(actual)
    {
    }

    /// <summary>
    /// TODO
    /// </summary>
    public ContinueWith<DoesString> Contain(string expected)
        => new DoesString(_actual).Contain(expected);

    /// <summary>
    /// TODO
    /// </summary>
    public ContinueWith<DoesString> NotContain(string other)
        => new DoesString(_actual).NotContain(other);
}