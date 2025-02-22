using XspecT.Assert.Continuations.Numerical.Fractional;
using XspecT.Assert.Continuations.Numerical.Nullable;

namespace XspecT.Assert.Continuations.Numerical.Fractional.Nullable;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable double
/// </summary>
public record IsNullableDouble : IsNullableNumerical<double, IsNullableDouble, IsDouble>;