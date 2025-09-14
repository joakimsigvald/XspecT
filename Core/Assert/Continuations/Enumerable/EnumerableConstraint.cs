namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public abstract record EnumerableConstraint<TItem, TContinuation> : Constraint<IEnumerable<TItem>, TContinuation>
    where TContinuation : EnumerableConstraint<TItem, TContinuation>, new()
{
    static readonly string[] methodsWithCount = ["Single", "Count", "OneItem", "TwoItems", "ThreeItems", "FourItems", "FiveItems"];

    private protected override string Describe(IEnumerable<TItem>? value, string? methodName = null) 
        => value is ICollection<TItem> col && methodsWithCount.Contains(methodName)
        ? $"{col.Count}: {base.Describe(value, methodName)}"
        : base.Describe(value, methodName);
}