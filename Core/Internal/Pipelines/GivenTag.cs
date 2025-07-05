using System.Runtime.CompilerServices;
using XspecT.Continuations;
using XspecT.Internal.Specification;

namespace XspecT.Internal.Pipelines;

internal class GivenTag<TSUT, TResult, TValue>(
    Spec<TSUT, TResult> spec, Tag<TValue> tag, string tagExpr)
    : IGivenTag<TSUT, TResult, TValue>
{
    public IGivenTestPipeline<TSUT, TResult> Is(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null)
        => spec.Apply<TValue>(() => spec.Assign(tag, value), $"{tagExpr} is {valueExpr!.ParseValue()}", true);

    public IGivenTestPipeline<TSUT, TResult> Has(
        Action<TValue> setup,
        [CallerArgumentExpression(nameof(setup))] string? setupExpr = null)
        => spec.Apply<TValue>(() => spec.Apply(tag, setup), $"{tagExpr} has {setupExpr!.ParseValue()}", true);

    public IGivenTestPipeline<TSUT, TResult> Has(
        Func<TValue, TValue> transform,
        [CallerArgumentExpression(nameof(transform))] string? transformExpr = null)
        => spec.Apply<TValue>(() => spec.Apply(tag, transform), $"{tagExpr} has {transformExpr!.ParseValue()}", true);
}