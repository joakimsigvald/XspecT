using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public class IsString : Constraint<IsStringContinuation, string>
{
    internal IsString(string actual) : base(actual) { }

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsStringContinuation> Like(string expected)
    {
        _actual.Should().BeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string is null
    /// </summary>
    public ContinueWith<IsStringContinuation> Null()
    {
        _actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// Asserts that the string is empty
    /// </summary>
    public ContinueWith<IsStringContinuation> Empty()
    {
        _actual.Should().BeEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotEmpty()
    {
        _actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNull()
    {
        _actual.Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// Asserts that the string is null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrEmpty()
    {
        _actual.Should().BeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null or empty
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
    {
        _actual.Should().NotBeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
    {
        _actual.Should().BeNullOrWhiteSpace();
        return And();
    }

    /// <summary>
    /// Asserts that the string contains non-whitespace characters
    /// </summary>
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
    {
        _actual.Should().NotBeNullOrWhiteSpace();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not the given value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public ContinueWith<IsStringContinuation> Not(string value)
    {
        _actual.Should().NotBe(value);
        return And();
    }

    internal override IsStringContinuation Continue() => new(_actual);
}