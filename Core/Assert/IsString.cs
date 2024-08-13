using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided string
/// </summary>
public record IsString : Constraint<IsStringContinuation, string>
{
    internal IsString(string actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> Like(string expected)
    {
        Actual.Should().BeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NotLike(string expected)
    {
        Actual.Should().NotBeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string is equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> EquivalentTo(string expected)
    {
        Actual.Should().BeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string is not equivalent to expected, ignoring casing and leading or trailing whitespace
    /// actual.Should().NotBeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NotEquivalentTo(string expected)
    {
        Actual.Should().NotBeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the string is null
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> Null()
    {
        Actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// Asserts that the string is empty
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> Empty()
    {
        Actual.Should().BeEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not empty
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NotEmpty()
    {
        Actual.Should().NotBeEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NotNull()
    {
        Actual.Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// Asserts that the string is null or empty
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NullOrEmpty()
    {
        Actual.Should().BeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not null or empty
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NotNullOrEmpty()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNullOrEmpty(), "is not null or empty");
        Actual.Should().NotBeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// Asserts that the string does not contain non-whitespace characters
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NullOrWhitespace()
    {
        Actual.Should().BeNullOrWhiteSpace();
        return And();
    }

    /// <summary>
    /// Asserts that the string contains non-whitespace characters
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> NotNullOrWhitespace()
    {
        Actual.Should().NotBeNullOrWhiteSpace();
        return And();
    }

    /// <summary>
    /// Asserts that the string is not the given value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> Not(string value)
    {
        Actual.Should().NotBe(value);
        return And();
    }

    /// <summary>
    /// Asserts that the string starts with a prefix
    /// </summary>
    /// <param name="prefix"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> StartingWith(string prefix)
    {
        Actual.Should().StartWith(prefix);
        return And();
    }

    /// <summary>
    /// Asserts that the string ends with a suffix
    /// </summary>
    /// <param name="suffix"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsStringContinuation> EndingWith(string suffix)
    {
        Actual.Should().EndWith(suffix);
        return And();
    }

    internal override IsStringContinuation Continue() => new(Actual);
}