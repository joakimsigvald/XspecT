using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable TimeSpan
/// </summary>
public record IsNullableTimeSpan : Constraint<TimeSpan?, IsNullableTimeSpan>
{
    internal IsNullableTimeSpan(TimeSpan? actual, string actualExpr = null) : base(actual, actualExpr) { }

    /// <summary>
    /// Asserts that the timeSpan is null or not equal to the given value
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<IsNullableTimeSpan> Not(
        TimeSpan expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the timeSpan is not null
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsTimeSpan> NotNull()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNull());
        return new(new(Actual.Value));
    }

    /// <summary>
    /// Asserts that the timeSpan is null
    /// </summary>
    [CustomAssertion]
    public void Null()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeNull());
    }
}