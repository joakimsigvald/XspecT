namespace XspecT.Assert;

/// <summary>
/// Object that allows another assertions to be made on the provided string
/// </summary>
public record HasEnumerableContinuation<TItem> : HasEnumerable<TItem>
{
    /// <summary>
    /// Continuation to assert that the string is satisfying some expectation
    /// </summary>
    /// <returns></returns>
    public IsEnumerable<TItem> Is() => Actual.Is(actualExpr: ActualExpr);
}