namespace XspecT.Assert.Numerical;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable uint
/// </summary>
public record IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt, IsUInt>;