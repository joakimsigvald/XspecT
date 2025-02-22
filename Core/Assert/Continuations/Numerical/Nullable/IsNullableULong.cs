using XspecT.Assert.Continuations.Numerical;

namespace XspecT.Assert.Continuations.Numerical.Nullable;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable ulong
/// </summary>
public record IsNullableULong : IsNullableNumerical<ulong, IsNullableULong, IsULong>;