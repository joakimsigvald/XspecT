namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public abstract record EnumerableConstraint<TItem, TContinuation> : Constraint<IEnumerable<TItem>, TContinuation>
    where TContinuation : EnumerableConstraint<TItem, TContinuation>, new()
{
    private protected override string Describe(IEnumerable<TItem>? value)
        => value is null ? "null"
        : value.Any() ? $"[{string.Join(", ", value)}]"
        : "[]";
}