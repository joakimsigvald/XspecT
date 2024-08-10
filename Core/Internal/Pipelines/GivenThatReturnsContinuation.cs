using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenThatReturnsContinuation<TSUT, TResult, TService>
    : GivenTestPipeline<TSUT, TResult>, IGivenThatReturnsContinuation<TSUT, TResult, TService>
    where TService : class
{
    private readonly GivenServiceContinuation<TSUT, TResult, TService> _serviceContinuation;

    internal GivenThatReturnsContinuation(Spec<TSUT, TResult> spec) : base(spec) => _serviceContinuation = new(spec);

    public IGivenTestPipeline<TSUT, TResult> AndReturnsDefault<TReturns>(Func<TReturns> value)
        => _serviceContinuation.Returns(value);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> AndThat<TReturns>(
        Expression<Func<TService, TReturns>> call,
        [CallerArgumentExpression(nameof(call))] string callExpr = null)
        => _serviceContinuation.That(call, callExpr);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> AndThat<TReturns>(
        Expression<Func<TService, Task<TReturns>>> call,
        [CallerArgumentExpression(nameof(call))] string callExpr = null) 
        => _serviceContinuation.That(call, callExpr);
}