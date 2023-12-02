using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable DateTime
/// </summary>
public class IsNullableDateTime : Constraint<IsNullableDateTime, DateTime?>
{
    internal IsNullableDateTime(DateTime? actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    public ContinueWith<IsNullableDateTime> Not(DateTime unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotNull()
    {
        _actual.Should().NotBeNull();
        return new(new(_actual.Value));
    }

    /// <summary>
    /// TODO
    /// </summary>
    public void Null() => _actual.Should().BeNull();
}