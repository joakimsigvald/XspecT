namespace XspecT.Assert;

/// <summary>
/// Object that allows another assertions to be made on the provided string
/// </summary>
public record IsEnumerableContinuation<TItem> : IsEnumerable<TItem>
{
    /// <summary>
    /// Continuation to assert that the string is satisfying some expectation
    /// </summary>
    /// <returns></returns>
    public HasEnumerable<TItem> Has() => Actual.Has(actualExpr: ActualExpr);
}