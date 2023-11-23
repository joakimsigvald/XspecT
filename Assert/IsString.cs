using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// TODO
/// </summary>
public class IsString : Constraint<IsStringContinuation, string>
{
    internal IsString(string actual) : base(actual) { }

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> Like(string expected)
    {
        _actual.Should().BeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeNull(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> Null()
    {
        _actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().BeEmpty(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> Empty()
    {
        _actual.Should().BeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeEmpty(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEmpty()
    {
        _actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeNull(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNull()
    {
        _actual.Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().BeNullOrEmpty(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrEmpty()
    {
        _actual.Should().BeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeNullOrEmpty(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
    {
        _actual.Should().NotBeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().BeNullOrWhiteSpace(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
    {
        _actual.Should().BeNullOrWhiteSpace();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeNullOrWhiteSpace(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
    {
        _actual.Should().NotBeNullOrWhiteSpace();
        return And();
    }

    internal override IsStringContinuation Continue() => new(_actual);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public ContinueWith<IsStringContinuation> Not(string value)
    {
        _actual.Should().NotBe(value);
        return And();
    }
}