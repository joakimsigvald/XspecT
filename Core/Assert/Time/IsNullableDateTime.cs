using System.Runtime.CompilerServices;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable DateTime
/// </summary>
public record IsNullableDateTime : Constraint<DateTime?, IsNullableDateTime>
{
    internal IsNullableDateTime(DateTime? actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the dateTime is null or not equal to the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsNullableDateTime> Not(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => AssertAnd(() => Actual.Should().NotBe(expected), expectedExpr);

    /// <summary>
    /// Asserts that the dateTime is not null
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotNull()
    {
        Assert(() => Actual.Should().NotBeNull());
        return AndValue();
    }

    /// <summary>
    /// Asserts that the dateTime is null
    /// </summary>
    public void Null()
    {
        Assert(() => Actual.Should().BeNull());
    }

    /// <summary>
    /// Asserts that the nullable dateTime is before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> Before(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().BeBefore(expected), expectedExpr);
        return AndValue();
    }

    /// <summary>
    /// Asserts that the nullable dateTime is after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> After(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().BeAfter(expected), expectedExpr);
        return AndValue();
    }

    /// <summary>
    /// Asserts that the nullable dateTime is not before the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotBefore(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().NotBeBefore(expected), expectedExpr);
        return AndValue();
    }

    /// <summary>
    /// Asserts that the nullable dateTime is not after the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotAfter(
        DateTime expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        Assert(() => Actual.Should().NotBeAfter(expected), expectedExpr);
        return AndValue();
    }

    private ContinueWith<IsDateTime> AndValue() => new(new(Actual.Value));

}