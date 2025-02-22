using XspecT.Assert.Continuations.Numerical;

namespace XspecT.Assert.Continuations.Numerical.Nullable;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable uint
/// </summary>
public record IsNullableUInt : IsNullableNumerical<uint, IsNullableUInt, IsUInt>;