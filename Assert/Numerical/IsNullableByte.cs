﻿using FluentAssertions;

namespace XspecT.Assert.Numerical;

/// <summary>
/// TODO
/// </summary>
public class IsNullableByte : IsNullableNumerical<byte, IsNullableByte>
{
    internal IsNullableByte(byte? actual) : base(actual) { }
    internal override FluentAssertions.Numeric.NullableNumericAssertions<byte> Should() => _actual.Should();
}