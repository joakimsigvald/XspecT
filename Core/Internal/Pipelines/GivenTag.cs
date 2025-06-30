using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenTag<TSUT, TResult, TValue>(
    Spec<TSUT, TResult> spec, Tag<TValue> tag, string tagExpr)
    : IGivenTag<TSUT, TResult, TValue>
{
    public IGivenTestPipeline<TSUT, TResult> Is(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null)
        => spec.SetupMention<TValue>(() => spec.The(tag, value), $"{tagExpr} is {valueExpr!}", string.Empty);

    public IGivenTestPipeline<TSUT, TResult> IsDefault()
        => spec.Given().Default(() => spec.The(tag), tagExpr);

    public IGivenTestPipeline<TSUT, TResult> IsUsed()
        => spec.Given().Using(() => spec.The(tag), tagExpr);
}