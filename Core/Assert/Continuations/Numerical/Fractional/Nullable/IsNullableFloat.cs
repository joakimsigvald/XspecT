using XspecT.Assert.Continuations.Numerical.Fractional;
using XspecT.Assert.Continuations.Numerical.Nullable;

namespace XspecT.Assert.Continuations.Numerical.Fractional.Nullable;

/// <summary>
/// Object that allows an assertions to be made on the provided nullable float
/// </summary>
public record IsNullableFloat : IsNullableNumerical<float, IsNullableFloat, IsFloat>;