using System.Runtime.CompilerServices;

namespace XspecT.Assert.Continuations.Enumerable;

/// <summary>
/// Continuation that allows an assertions to be made on the provided enumerable
/// </summary>
/// <typeparam name="TItem"></typeparam>
public record DoesEnumerable<TItem> : EnumerableConstraint<TItem, DoesEnumerableContinuation<TItem>>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<DoesEnumerableContinuation<TItem>> Contain(
        TItem expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert($"{expected}",
            () => Xunit.Assert.Contains(expected, Actual),
            expectedExpr, "", "contains").And();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="expectedExpr"></param>
    /// <returns></returns>
    public ContinueWith<DoesEnumerableContinuation<TItem>> NotContain(
        TItem expected, [CallerArgumentExpression(nameof(expected))] string expectedExpr = null)
        => Assert($"{expected}", () => Xunit.Assert.DoesNotContain(expected, Actual), expectedExpr, "").And();

    internal override DoesEnumerableContinuation<TItem> Continue() => Create(Actual);
}