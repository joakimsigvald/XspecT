using FluentAssertions;

namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided object
/// </summary>
public class IsObject : Constraint<IsObject, object>
{
    internal IsObject(object actual) : base(actual) { }

    /// <summary>
    /// Should().NotBeSameAs(expected)
    /// </summary>
    public ContinueWith<IsObject> Not(object expected)
    {
        _actual.Should().NotBeSameAs(expected);
        return And();
    }

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    public ContinueWith<IsObject> Null()
    {
        _actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    public ContinueWith<IsObject> NotNull()
    {
        _actual.Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// Should().Be(expected)
    /// </summary>
    public ContinueWith<IsObject> EqualTo(object expected)
    {
        _actual.Should().Be(expected);
        return And();
    }

    /// <summary>
    /// Should().NotBe(expected)
    /// </summary>
    public ContinueWith<IsObject> NotEqualTo(object expected)
    {
        _actual.Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    public ContinueWith<IsObject> Like(object expected)
    {
        _actual.Should().BeEquivalentTo(expected);
        return And();
    }
}