using XspecT.Assert.Continuations;

namespace XspecT.Assert.Continuations.Numerical.Nullable;

/// <summary>
/// Base class that allows an assertions to be made on the provided numerical
/// </summary>
/// <typeparam name="TContinuation"></typeparam>
/// <typeparam name="TActual"></typeparam>
public abstract record IsNumerical<TActual, TContinuation> : IsComparable<TActual, TContinuation>
    where TContinuation : IsNumerical<TActual, TContinuation>, new()
    where TActual : struct, IComparable<TActual>;