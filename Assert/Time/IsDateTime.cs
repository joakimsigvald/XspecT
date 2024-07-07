using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided DateTime
/// </summary>
public class IsDateTime : Constraint<IsDateTime, DateTime>
{
    internal IsDateTime(DateTime actual) : base(actual) { }

    /// <summary>
    /// Asserts that the dateTime is not equal to the given value
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDateTime> Not(DateTime unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDateTime> Before(DateTime expected)
    {
        _actual.Should().BeBefore(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDateTime> After(DateTime expected)
    {
        _actual.Should().BeAfter(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is at or after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDateTime> NotBefore(DateTime expected)
    {
        _actual.Should().NotBeBefore(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is at or before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDateTime> NotAfter(DateTime expected)
    {
        _actual.Should().NotBeAfter(expected);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is within the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDateTime> CloseTo(DateTime expected, TimeSpan precision)
    {
        _actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime differ more than the specified precision time from the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    [CustomAssertion]
    public ContinueWith<IsDateTime> NotCloseTo(DateTime expected, TimeSpan precision)
    {
        _actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }
}