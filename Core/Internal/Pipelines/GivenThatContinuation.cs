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

    public IGivenTestPipeline<TSUT, TResult> Returns([NotNull] Func<TReturns> returns)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        SetupReturns(returns);
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenTestPipeline<TSUT, TResult> ReturnsDefault()
    {
        SetupReturns(() => default);
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenTestPipeline<TSUT, TResult> Throws<TException>()
        where TException : Exception, new()
    {
        _spec.GivenSetup(() => _continuation.Throws<TException>());
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenTestPipeline<TSUT, TResult> Throws(Func<Exception> ex)
    {
        _spec.GivenSetup(() => _continuation.Throws(ex()));
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    private void SetupReturns(Func<TReturns> returns)
        => _spec.GivenSetup(() => DoSetupReturns(returns));

    protected void DoSetupReturns(Func<TReturns> returns)
    {
        if (_continuation is Moq.Language.Flow.IReturnsThrows<TService, Task<TReturns>> asyncContinuation)
            asyncContinuation.ReturnsAsync(returns);
        else
            _continuation.Returns(returns);
    }
}

internal class GivenThatContinuation<TSUT, TResult, TService, TReturns, TActualReturns>
    : GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>,
    IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    private Moq.Language.Flow.ISetup<TService, TActualReturns> _setup;

    internal GivenThatContinuation(
        Spec<TSUT, TResult> spec, Moq.Language.Flow.ISetup<TService, TActualReturns> setup)
        : base(spec, setup)
        => _setup = setup;

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap(Action callback)
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>(
            _spec, _setup.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg>(Action<TArg> callback)
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>(
            _spec, _setup.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2>(
        Action<TArg1, TArg2> callback)
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>(
            _spec, _setup.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3>(
        Action<TArg1, TArg2, TArg3> callback)
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>(
            _spec, _setup.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4>(
        Action<TArg1, TArg2, TArg3, TArg4> callback)
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>(
            _spec, _setup.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4, TArg5>(
        Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback)
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns>(
            _spec, _setup.Callback(callback));
}