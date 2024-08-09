using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TMock>
    : IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
    where TMock : Moq.Language.Flow.IReturnsThrows<TService, TActualReturns>
{
    protected readonly Spec<TSUT, TResult> _spec;
    protected readonly Lazy<TMock> _lazyContinuation;
    protected readonly string _callExpr;
    protected readonly string _tapExpr;

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec, Lazy<TMock> continuation, string callExpr, string tapExpr = null)
    {
        _spec = spec;
        _lazyContinuation = continuation;
        _callExpr = callExpr;
        _tapExpr = tapExpr;
    }

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec, 
        Expression<Func<TService, TActualReturns>> call, 
        string callExpr = null)
        : this(spec, new Lazy<TMock>(() => (TMock)spec.GetMock<TService>().Setup(call)), callExpr)
    {
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns(
        [NotNull] Func<TReturns> returns, [CallerArgumentExpression("returns")] string returnsExpr = null)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        SetupReturns(returns, returnsExpr);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> ReturnsDefault()
        => Returns(() => default);

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws<TException>()
        where TException : Exception, new()
    {
        _spec.ArrangeLast(() => Continuation.Throws<TException>());
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws(Func<Exception> ex)
    {
        _spec.ArrangeLast(() => Continuation.Throws(ex()));
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    protected TMock Continuation => _lazyContinuation.Value;

    private void SetupReturns(Func<TReturns> returns, string returnsExpr)
    {
        _spec.ArrangeLast(DoSetupReturns);

        void DoSetupReturns()
        {
            if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncContinuation)
                asyncContinuation.ReturnsAsync(returns);
            else
                Continuation.Returns(returns);
            if (_callExpr is not null)
                Specification.AddMockSetup<TService>(_callExpr);
            if (_tapExpr is not null)
                Specification.AddTap(_tapExpr);
            Specification.AddMockReturns(returnsExpr);
        }
    }
}