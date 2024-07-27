using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenThatContinuation<TSUT, TResult, TService, TReturns, TActualReturns>
    : GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns, Moq.Language.Flow.ISetup<TService, TActualReturns>>,
    IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    internal GivenThatContinuation(
        Spec<TSUT, TResult> spec, Expression<Func<TService, TActualReturns>> expression)
        : base(spec, expression) { }

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap(Action callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg>(Action<TArg> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TArg>([NotNull] Func<TArg, TReturns> returns)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        TReturns retVal = default;
        var continuation = ContinueWith(() => Continuation.Callback(callback));
        return continuation.Returns(() => retVal);

        void callback(TArg arg) => retVal = returns(arg);
    }

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2>(
        Action<TArg1, TArg2> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TArg1, TArg2>([NotNull] Func<TArg1, TArg2, TReturns> returns)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        TReturns retVal = default;
        var continuation = ContinueWith(() => Continuation.Callback(callback));
        return continuation.Returns(() => retVal);

        void callback(TArg1 arg1, TArg2 arg2) => retVal = returns(arg1, arg2);
    }

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3>(
        Action<TArg1, TArg2, TArg3> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TArg1, TArg2, TArg3>([NotNull] Func<TArg1, TArg2, TArg3, TReturns> returns)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        TReturns retVal = default;
        var continuation = ContinueWith(() => Continuation.Callback(callback));
        return continuation.Returns(() => retVal);

        void callback(TArg1 arg1, TArg2 arg2, TArg3 arg3) => retVal = returns(arg1, arg2, arg3);
    }

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4>(
        Action<TArg1, TArg2, TArg3, TArg4> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TArg1, TArg2, TArg3, TArg4>([NotNull] Func<TArg1, TArg2, TArg3, TArg4, TReturns> returns)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        TReturns retVal = default;
        var continuation = ContinueWith(() => Continuation.Callback(callback));
        return continuation.Returns(() => retVal);

        void callback(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) => retVal = returns(arg1, arg2, arg3, arg4);
    }

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4, TArg5>(
        Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TArg1, TArg2, TArg3, TArg4, TArg5>([NotNull] Func<TArg1, TArg2, TArg3, TArg4, TArg5, TReturns> returns)
    {
        if (returns is null)
            throw new SetupFailed($"{nameof(returns)} may not be null");
        TReturns retVal = default;
        var continuation = ContinueWith(() => Continuation.Callback(callback));
        return continuation.Returns(() => retVal);

        void callback(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) => retVal = returns(arg1, arg2, arg3, arg4, arg5);
    }

    public IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> ContinueWith<TMock>(Func<TMock> callback)
        where TMock : Moq.Language.Flow.IReturnsThrows<TService, TActualReturns>
        => new GivenThatCommonContinuation<TSUT, TResult, TService, TReturns, TActualReturns, TMock>(_spec, new Lazy<TMock>(callback));
}