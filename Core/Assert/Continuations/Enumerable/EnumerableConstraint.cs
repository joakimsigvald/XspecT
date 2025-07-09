namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Object that allows an assertions to be made on the provided enumerable
/// </summary>
public abstract record EnumerableConstraint<TItem, TContinuation> : Constraint<IEnumerable<TItem>, TContinuation>
    where TContinuation : EnumerableConstraint<TItem, TContinuation>, new()
{
    static readonly string[] methodsWithCount = ["Single", "Count"];

    private protected override string Describe(IEnumerable<TItem>? value, string? methodName = null)
    {
        if (value is null) return "null";
        ICollection<TItem> enumeratedValue = value as ICollection<TItem> ?? [.. value];
        return methodsWithCount.Contains(methodName)
            ? $"{enumeratedValue.Count}: [{string.Join(", ", enumeratedValue)}]" 
            : $"[{string.Join(", ", value)}]";
    }
}