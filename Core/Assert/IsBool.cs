namespace XspecT.Assert;

/// <summary>
/// Object that allows an assertions to be made on the provided bool
/// </summary>
public record IsBool : Constraint<bool, IsBool>
{
    internal IsBool(bool actual, string actualExpr) : base(actual, actualExpr) { }

    /// <summary>
    /// actual.Should().BeTrue()
    /// </summary>
    public ContinueWith<IsBool> True()
        => AssertAnd(() => Xunit.Assert.True(Actual));

    /// <summary>
    /// actual.Should().BeFalse()
    /// </summary>
    public ContinueWith<IsBool> False()
        => AssertAnd(() => Xunit.Assert.False(Actual));
}

/// <summary>
/// Object that allows an assertions to be made on the provided nullable Enum
/// </summary>
public record IsNullableStruct<TValue> : Constraint<TValue?, IsNullableStruct<TValue>>
    where TValue : struct
{
    internal IsNullableStruct(TValue? actual, string actualExpr = null) : base(actual, actualExpr) { }
}