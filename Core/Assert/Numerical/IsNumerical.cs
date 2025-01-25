namespace XspecT.Assert.Numerical;

/// <summary>
/// Base class that allows an assertions to be made on the provided numerical
/// </summary>
/// <typeparam name="TContinuation"></typeparam>
/// <typeparam name="TActual"></typeparam>
public abstract record IsNumerical<TActual, TContinuation> : IsComparable<TActual, TContinuation>
    where TContinuation : IsNumerical<TActual, TContinuation>
    where TActual : struct, IComparable<TActual>
{
    internal IsNumerical(TActual actual, string actualExpr = null) : base(actual, actualExpr) { }
}