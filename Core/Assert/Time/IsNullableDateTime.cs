using FluentAssertions;
using System.Runtime.CompilerServices;

namespace XspecT.Assert.Time;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable DateTime
/// </summary>
public record IsNullableDateTime : Constraint<IsNullableDateTime, DateTime?>
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
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBe(expected), expectedExpr);
        return And();
    }

    /// <summary>
    /// Asserts that the dateTime is not null
    /// </summary>
    /// <returns></returns>
    public ContinueWith<IsDateTime> NotNull()
    {
        AddAssert([CustomAssertion] () => Actual.Should().NotBeNull());
        return new(new(Actual.Value));
    }

    /// <summary>
    /// Asserts that the dateTime is null
    /// </summary>
    public void Null()
    {
        AddAssert([CustomAssertion] () => Actual.Should().BeNull());
    }
}