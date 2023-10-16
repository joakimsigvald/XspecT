using FluentAssertions;

namespace XspecT.Verification.Assertions;

public abstract class IsNullableNumerical<TActual, TConstrain> : Constraint<TConstrain>
    where TActual : struct, IComparable<TActual>
    where TConstrain : IsNullableNumerical<TActual, TConstrain>
{
    protected readonly TActual? Actual;

    protected IsNullableNumerical(TActual? actual) => Actual = actual;

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstrain> Null()
    {
        Should().BeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstrain> NotNull()
    {
        Should().NotBeNull();
        return And();
    }

    /// <summary>
    /// actual.Should().NotBe(expected)
    /// </summary>
    public ContinueWith<TConstrain> Not(TActual? expected)
    {
        Should().NotBe(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThan(expected)
    /// </summary>
    public ContinueWith<TConstrain> GreaterThan(TActual expected)
    {
        Should().BeGreaterThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThan(expected)
    /// </summary>
    public ContinueWith<TConstrain> LessThan(TActual expected)
    {
        Should().BeLessThan(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeLessThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstrain> NotGreaterThan(TActual expected)
    {
        Should().BeLessThanOrEqualTo(expected);
        return And();
    }

    /// <summary>
    /// actual.Should().BeGreaterThanOrEqualTo(expected)
    /// </summary>
    public ContinueWith<TConstrain> NotLessThan(TActual expected)
    {
        Should().BeGreaterThanOrEqualTo(expected);
        return And();
    }

    protected abstract FluentAssertions.Numeric.NullableNumericAssertions<TActual> Should();
}

public class IsNullableByte : IsNullableNumerical<byte, IsNullableByte>
{
    public IsNullableByte(byte? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<byte> Should() => Actual.Should();
}

public class IsNullableSByte : IsNullableNumerical<sbyte, IsNullableSByte>
{
    public IsNullableSByte(sbyte? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<sbyte> Should() => Actual.Should();
}

public class IsNullableShort : IsNullableNumerical<short, IsNullableShort>
{
    public IsNullableShort(short? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<short> Should() => Actual.Should();
}

public class IsNullableUShort : IsNullableNumerical<ushort, IsNullableUShort>
{
    public IsNullableUShort(ushort? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ushort> Should() => Actual.Should();
}

public class IsNullableInt : IsNullableNumerical<int, IsNullableInt>
{
    public IsNullableInt(int? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<int> Should() => Actual.Should();
}

public class IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt>
{
    public IsNullableUInt(uint? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<uint> Should() => Actual.Should();
}

public class IsNullableLong : IsNullableNumerical<long, IsNullableLong>
{
    public IsNullableLong(long? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<long> Should() => Actual.Should();
}

public class IsNullableULong : IsNullableNumerical<ulong, IsNullableULong>
{
    public IsNullableULong(ulong? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<ulong> Should() => Actual.Should();
}

public class IsNullableFloat : IsNullableNumerical<float, IsNullableFloat>
{
    public IsNullableFloat(float? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<float> Should() => Actual.Should();
}

public class IsNullableDouble : IsNullableNumerical<double, IsNullableDouble>
{
    public IsNullableDouble(double? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<double> Should() => Actual.Should();
}

public class IsNullableDecimal : IsNullableNumerical<decimal, IsNullableDecimal>
{
    public IsNullableDecimal(decimal? actual) : base(actual) { }
    protected override FluentAssertions.Numeric.NullableNumericAssertions<decimal> Should() => Actual.Should();
}