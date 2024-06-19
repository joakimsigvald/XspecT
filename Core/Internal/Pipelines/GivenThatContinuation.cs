using Moq;
using System.Diagnostics.CodeAnalysis;

namespace XspecT.Internal.Pipelines;

internal class GivenThatContinuation<TSUT, TResult, TService, TReturns, TActualReturns>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    private readonly Spec<TSUT, TResult> _spec;
    protected readonly Moq.Language.Flow.ISetup<TService, TActualReturns> _setup;

    internal GivenThatContinuation(
        Spec<TSUT, TResult> spec, Moq.Language.Flow.ISetup<TService, TActualReturns> setup)
    {
        _spec = spec;
        _setup = setup;
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
        _spec.GivenSetup(() => _setup.Throws<TException>());
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenTestPipeline<TSUT, TResult> Throws(Func<Exception> ex)
    {
        _spec.GivenSetup(() => _setup.Throws(ex()));
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    private void SetupReturns(Func<TReturns> returns) 
        => _spec.GivenSetup(() => DoSetupReturns(returns));

    protected virtual void DoSetupReturns(Func<TReturns> returns)
    {
        if (typeof(TActualReturns) == typeof(Task<TReturns>))
            ((Moq.Language.Flow.ISetup<TService, Task<TReturns>>)_setup).ReturnsAsync(returns);
        else _setup.Returns(returns);
    }

    public IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg> Tap<TArg>(Action<TArg> callback)
        => new GivenTappedContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TArg>(_spec, _setup, callback);

    public IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg1, TArg2> Tap<TArg1, TArg2>(
        Action<TArg1, TArg2> callback)
        => new GivenTappedContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TArg1, TArg2>(
            _spec, _setup, callback);
}

internal class GivenTappedContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TArg>
    : GivenThatContinuation<TSUT, TResult, TService, TReturns, TActualReturns>,
    IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg>
    where TService : class
{
    private readonly Action<TArg> _callback;

    internal GivenTappedContinuation(
        Spec<TSUT, TResult> spec,
        Moq.Language.Flow.ISetup<TService, TActualReturns> setup,
        Action<TArg> callback)
        : base(spec, setup) => _callback = callback;

    protected override void DoSetupReturns(Func<TReturns> returns)
    {
        if (typeof(TActualReturns) == typeof(Task<TReturns>))
            ((Moq.Language.Flow.ISetup<TService, Task<TReturns>>)_setup)
                .Callback(_callback)
                .ReturnsAsync(returns);
        else _setup
                .Callback(_callback)
                .Returns(returns);
    }
}
internal class GivenTappedContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TArg1, TArg2>
    : GivenThatContinuation<TSUT, TResult, TService, TReturns, TActualReturns>,
    IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg1, TArg2>
    where TService : class
{
    private readonly Action<TArg1, TArg2> _callback;

    internal GivenTappedContinuation(
        Spec<TSUT, TResult> spec,
        Moq.Language.Flow.ISetup<TService, TActualReturns> setup,
        Action<TArg1, TArg2> callback)
        : base(spec, setup) => _callback = callback;

    protected override void DoSetupReturns(Func<TReturns> returns)
    {
        if (typeof(TActualReturns) == typeof(Task<TReturns>))
            ((Moq.Language.Flow.ISetup<TService, Task<TReturns>>)_setup)
                .Callback(_callback)
                .ReturnsAsync(returns);
        else _setup
                .Callback(_callback)
                .Returns(returns);
    }
}