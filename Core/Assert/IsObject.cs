using FluentAssertions;
using XspecT.Internal.TestData;
using Xunit.Sdk;

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
    [CustomAssertion]
    public ContinueWith<IsObject> Not(object expected)
    {
        try
        {
            _actual.Should().NotBeSameAs(expected);
        }
        catch (XunitException ex)
        {
            throw new XunitException(Context.Specification, ex);
        }
        return And();
    }

    /// <summary>
    /// Should().BeNull()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> Null()
    {
        _actual.Should().BeNull();
        return And();
    }

    /// <summary>
    /// Should().NotBeNull()
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> NotNull()
    {
        _actual.Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// Should().Be(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> EqualTo(object expected)
    {
        _actual.Should().Be(expected);
        return And();
    }

    /// <summary>
    /// Should().NotBe(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> NotEqualTo(object expected)
    {
        _actual.Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> Like(object expected)
    {
        _actual.Should().BeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Should().NotBeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> NotLike(object expected)
    {
        _actual.Should().NotBeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> EquivalentTo(object expected)
    {
        _actual.Should().BeEquivalentTo(expected);
        return And();
    }

    /// <summary>
    /// Should().BeEquivalentTo(expected)
    /// </summary>
    [CustomAssertion]
    public ContinueWith<IsObject> NotEquivalentTo(object expected)
    {
        _actual.Should().NotBeEquivalentTo(expected);
        return And();
    }
}