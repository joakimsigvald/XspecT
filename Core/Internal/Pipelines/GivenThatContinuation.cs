namespace XspecT.Internal.Pipelines;

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