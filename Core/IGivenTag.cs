using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT;

/// <summary>
/// Continuation for setting up an expectation for a tag, such as associating it with a value
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TValue"></typeparam>
public interface IGivenTag<TSUT, TResult, TValue> 
{
    /// <summary>
    /// Associate a tag with a value, which can be referenced in the pipeline using 'The([tag])'
    /// </summary>
    /// <param name="value"></param>
    /// <param name="valueExpr"></param>
    /// <returns>The pipeline, so that further setup can be provided</returns>
    IGivenTestPipeline<TSUT, TResult> Is(
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? valueExpr = null);
}