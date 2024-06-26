﻿namespace XspecT.Internal.Pipelines;

internal class GivenThatContinuation<TSUT, TResult, TService, TReturns, TActualReturns>
    : GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns, Moq.Language.Flow.ISetup<TService, TActualReturns>>,
    IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    internal GivenThatContinuation(
        Spec<TSUT, TResult> spec, Lazy<Moq.Language.Flow.ISetup<TService, TActualReturns>> setup)
        : base(spec, setup) { }

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap(Action callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg>(Action<TArg> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2>(
        Action<TArg1, TArg2> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3>(
        Action<TArg1, TArg2, TArg3> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4>(
        Action<TArg1, TArg2, TArg3, TArg4> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4, TArg5>(
        Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> ContinueWith<TMock>(Func<TMock> callback)
        where TMock : Moq.Language.Flow.IReturnsThrows<TService, TActualReturns>
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TMock>(_spec, new Lazy<TMock>(callback));
}