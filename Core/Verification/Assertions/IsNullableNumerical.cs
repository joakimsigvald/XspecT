using FluentAssertions;

namespace XspecT.Verification.Assertions;

public abstract class IsNullableNumerical<TActual> where TActual : struct, IComparable<TActual>
{
    protected readonly TActual? Actual;

    protected IsNullableNumerical(TActual? actual) => Actual = actual;

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.NullableNumericAssertions<TActual>> Not(TActual? expected)
        => Should().NotBe(expected);

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.NullableNumericAssertions<TActual>> GreaterThan(TActual expected)
        => Should().BeGreaterThan(expected);

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.NullableNumericAssertions<TActual>> LessThan(TActual expected)
        => Should().BeLessThan(expected);

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.NullableNumericAssertions<TActual>> NotGreaterThan(TActual expected)
        => Should().BeLessThanOrEqualTo(expected);

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public AndConstraint<FluentAssertions.Numeric.NullableNumericAssertions<TActual>> NotLessThan(TActual expected)
        => Should().BeGreaterThanOrEqualTo(expected);

    protected abstract FluentAssertions.Numeric.NullableNumericAssertions<TActual> Should();
}

public class IsNullableByte : IsNullableNumerical<byte>
{
    public IsNullableByte(byte? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<byte> Should() => Actual.Should();
}

public class IsNullableSByte : IsNullableNumerical<sbyte>
{
    public IsNullableSByte(sbyte? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<sbyte> Should() => Actual.Should();
}

public class IsNullableShort : IsNullableNumerical<short>
{
    public IsNullableShort(short? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<short> Should() => Actual.Should();
}

public class IsNullableUShort : IsNullableNumerical<ushort>
{
    public IsNullableUShort(ushort? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ushort> Should() => Actual.Should();
}

public class IsNullableInt : IsNullableNumerical<int>
{
    public IsNullableInt(int? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<int> Should() => Actual.Should();
}

public class IsNullableUInt : IsNullableNumerical<uint>
{
    public IsNullableUInt(uint? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<uint> Should() => Actual.Should();
}

public class IsNullableLong : IsNullableNumerical<long>
{
    public IsNullableLong(long? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<long> Should() => Actual.Should();
}

public class IsNullableULong : IsNullableNumerical<ulong>
{
    public IsNullableULong(ulong? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => Actual.Should();
}

public class IsNullableFloat : IsNullableNumerical<float>
{
    public IsNullableFloat(float? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<float> Should() => Actual.Should();
}

public class IsNullableDouble : IsNullableNumerical<double>
{
    public IsNullableDouble(double? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<double> Should() => Actual.Should();
}

public class IsNullableDecimal : IsNullableNumerical<decimal>
{
    public IsNullableDecimal(decimal? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<decimal> Should() => Actual.Should();
}