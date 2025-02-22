namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public abstract record EnumerableConstraint<TItem, TContinuation> : Constraint<IEnumerable<TItem>, TContinuation>
    where TContinuation : EnumerableConstraint<TItem, TContinuation>, new()
{
    private protected override string ActualString
        => Actual is null ? "null"
        : Actual.Any() ? $"[{string.Join(", ", Actual)}]"
        : "[]";
}