using Moq;
using System.Diagnostics.CodeAnalysis;

namespace XspecT.Internal.Pipelines;

internal class GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>
    : IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    protected readonly Spec<TSUT, TResult> _spec;
    protected readonly Moq.Language.Flow.IReturnsThrows<TService, TActualReturns> _continuation;

    internal GivenThatCommonContinuation(
        Spec<TSUT, TResult> spec, Moq.Language.Flow.IReturnsThrows<TService, TActualReturns> continuation)
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
        _spec.GivenSetup(() => _continuation.Throws<TException>());
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws(Func<Exception> ex)
    {
        _spec.GivenSetup(() => _continuation.Throws(ex()));
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    private void SetupReturns(Func<TReturns> returns) => _spec.GivenSetup(() => DoSetupReturns(returns));

    protected void DoSetupReturns(Func<TReturns> returns)
    {
        if (_continuation is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncContinuation)
            asyncContinuation.ReturnsAsync(returns);
        else
            _continuation.Returns(returns);
    }
}