using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Internal.TestData;

namespace XspecT.Internal.Pipelines;

internal class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TMock>
    : IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
    where TMock : Moq.Language.Flow.IReturnsThrows<TService, TActualReturns>
{
    protected readonly Spec<TSUT, TResult> _spec;
    protected readonly Expression<Func<TService, TActualReturns>> _expression;
    protected readonly Lazy<TMock> _lazyContinuation;

    internal GivenThatCommonContinuation(Spec<TSUT, TResult> spec, Lazy<TMock> continuation)
    {
        _spec = spec;
        _lazyContinuation = continuation;
    }

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec, Expression<Func<TService, TActualReturns>> expression)
    {
        _spec = spec;
        _expression = expression;
        _lazyContinuation = new Lazy<TMock>(() => (TMock)_spec.GetMock<TService>().Setup(expression));
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns([NotNull] Expression<Func<TReturns>> returns)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        SetupReturns(returns);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> ReturnsDefault()
    {
        SetupReturns(() => default);
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws<TException>()
        where TException : Exception, new()
    {
        _spec.GivenSetup(() => Continuation.Throws<TException>());
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws(Func<Exception> ex)
    {
        _spec.GivenSetup(() => Continuation.Throws(ex()));
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    protected TMock Continuation => _lazyContinuation.Value;

    private void SetupReturns(Expression<Func<TReturns>> returns) => _spec.GivenSetup(() => DoSetupReturns(returns));

    private void DoSetupReturns(Expression<Func<TReturns>> returns)
    {
        if (Continuation is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncContinuation)
            asyncContinuation.ReturnsAsync(returns.Compile());
        else
            Continuation.Returns(returns.Compile());
        if (_expression is not null)
            Specification.AddMockSetup(_expression);
        Specification.AddMockReturns(returns);
    }
}