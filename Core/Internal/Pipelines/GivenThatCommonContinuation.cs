using Moq;
using System.Diagnostics.CodeAnalysis;

namespace XspecT.Internal.Pipelines;

internal class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TMock>
    : IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
    where TMock : Moq.Language.Flow.IReturnsThrows<TService, TActualReturns>
{
    protected readonly Spec<TSUT, TResult> _spec;
    protected readonly Lazy<TMock> _continuation;

    internal GivenThatCommonContinuation(Spec<TSUT, TResult> spec, Lazy<TMock> continuation)
    {
        _spec = spec;
        _continuation = continuation;
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns([NotNull] Func<TReturns> returns)
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
        _spec.GivenSetup(() => _continuation.Value.Throws<TException>());
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws(Func<Exception> ex)
    {
        _spec.GivenSetup(() => _continuation.Value.Throws(ex()));
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    private void SetupReturns(Func<TReturns> returns) => _spec.GivenSetup(() => DoSetupReturns(returns));

    protected void DoSetupReturns(Func<TReturns> returns)
    {
        if (_continuation.Value is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncContinuation)
            asyncContinuation.ReturnsAsync(returns);
        else
            _continuation.Value.Returns(returns);
    }
}