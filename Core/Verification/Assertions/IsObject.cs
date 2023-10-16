using FluentAssertions;

namespace XspecT.Verification.Assertions;

public class IsObject : Constraint<IsObject, object>
{
    public IsObject(object actual) : base(actual) { }

    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsObject> Not(object expected)
    {
        Actual.Should().NotBeSameAs(expected);
        return And();
    }

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public ContinueWith<IsObject> Null()
    {
        Actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsObject> NotNull()
    {
        Actual.Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// Should().Be(expected)
    /// </summary>
    public ContinueWith<IsObject> EqualTo(object expected)
    {
        Actual.Should().Be(expected);
        return And();
    }

    /// <summary>
    /// Should().NotBe(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEqualTo(object expected)
    {
        Actual.Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> Like(object expected)
    {
        Actual.Should().BeEquivalentTo(expected);
        return And();
    }
}