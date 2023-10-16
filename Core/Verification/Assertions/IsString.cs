using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsString : Constraint<IsString, string>
{
    public IsString(string actual) : base(actual) { }

    /// <summary>
    /// actual.Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsString> Like(string expected)
    {
        Actual.Should().BeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeNull(expected)
    /// </summary>
    public ContinueWith<IsString> Null()
    {
        Actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeNull(expected)
    /// </summary>
    public ContinueWith<IsString> NotNull()
    {
        Actual.Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().BeNullOrEmpty(expected)
    /// </summary>
    public ContinueWith<IsString> NullOrEmpty()
    {
        Actual.Should().BeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeNullOrEmpty(expected)
    /// </summary>
    public ContinueWith<IsString> NotNullOrEmpty()
    {
        Actual.Should().NotBeNullOrEmpty();
        return And();
    }

    /// <summary>
    /// actual.Should().BeNullOrWhiteSpace(expected)
    /// </summary>
    public ContinueWith<IsString> NullOrWhitespace()
    {
        Actual.Should().BeNullOrWhiteSpace();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBeNullOrWhiteSpace(expected)
    /// </summary>
    public ContinueWith<IsString> NotNullOrWhitespace()
    {
        Actual.Should().NotBeNullOrWhiteSpace();
        return And();
    }
}