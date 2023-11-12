using FluentAssertions;

namespace XspecT.Assert.Time;

/// <summary>
/// TODO
/// </summary>
public class IsDateTime : Constraint<IsDateTime, DateTime>
{
    internal IsDateTime(DateTime actual) : base(actual) { }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="unexpected"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Not(DateTime unexpected)
    {
        _actual.Should().NotBe(unexpected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Before(DateTime expected)
    {
        _actual.Should().BeBefore(expected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> After(DateTime expected)
    {
        _actual.Should().BeAfter(expected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotBefore(DateTime expected)
    {
        _actual.Should().NotBeBefore(expected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotAfter(DateTime expected)
    {
        _actual.Should().NotBeAfter(expected);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> CloseTo(DateTime expected, TimeSpan precision)
    {
        _actual.Should().BeCloseTo(expected, precision);
        return And();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="precision"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotCloseTo(DateTime expected, TimeSpan precision)
    {
        _actual.Should().NotBeCloseTo(expected, precision);
        return And();
    }
}