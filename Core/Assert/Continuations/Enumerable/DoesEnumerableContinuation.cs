namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows another assertions to be made on the provided string
/// </summary>
public record DoesEnumerableContinuation<TItem> : DoesEnumerable<TItem>
{
    /// <summary>
    /// Continuation to assert that the string is satisfying some expectation
    /// </summary>
    /// <returns></returns>
    public HasEnumerable<TItem> Has() => Actual.Has(actualExpr: ActualExpr);
    /// <summary>
    /// Continuation to assert that the string is satisfying some expectation
    /// </summary>
    /// <returns></returns>
    public IsEnumerable<TItem> Is() => Actual.Is(actualExpr: ActualExpr);
}