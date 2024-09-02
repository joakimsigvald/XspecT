using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using XspecT.Continuations;

namespace XspecT.Internal.Pipelines;

internal class GivenThatVoidContinuation<TSUT, TResult, TService>
    : GivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void, Moq.Language.Flow.ISetup<TService>>,
    IGivenThatVoidContinuation<TSUT, TResult, TService>
    where TService : class
{
    internal GivenThatVoidContinuation(
        Spec<TSUT, TResult> spec,
        Expression<Action<TService>> call,
        string callExpr = null)
        : base(spec, call, callExpr) {}

    public IGivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void> Tap(
        Action callback,
        [CallerArgumentExpression(nameof(callback))] string callbackExpr = null)
        => ContinueWith(() => Continuation.Callback(callback), callbackExpr);

    public IGivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void> Tap<TArg>(
        Action<TArg> callback,
        [CallerArgumentExpression(nameof(callback))] string callbackExpr = null)
        => ContinueWith(() => Continuation.Callback(callback), callbackExpr);

    public IGivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void> Tap<TArg1, TArg2>(
        Action<TArg1, TArg2> callback,
        [CallerArgumentExpression(nameof(callback))] string callbackExpr = null)
        => ContinueWith(() => Continuation.Callback(callback), callbackExpr);

    public IGivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void> Tap<TArg1, TArg2, TArg3>(
        Action<TArg1, TArg2, TArg3> callback,
        [CallerArgumentExpression(nameof(callback))] string callbackExpr = null)
        => ContinueWith(() => Continuation.Callback(callback));

    public IGivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void> Tap<TArg1, TArg2, TArg3, TArg4>(
        Action<TArg1, TArg2, TArg3, TArg4> callback,
        [CallerArgumentExpression(nameof(callback))] string callbackExpr = null)
        => ContinueWith(() => Continuation.Callback(callback), callbackExpr);

    public IGivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void> Tap<TArg1, TArg2, TArg3, TArg4, TArg5>(
        Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback,
        [CallerArgumentExpression(nameof(callback))] string callbackExpr = null)
        => ContinueWith(() => Continuation.Callback(callback), callbackExpr);

    public IGivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void> ContinueWith<TMock>(
        Func<TMock> callback, string callbackExpr = null)
        where TMock : Moq.IFluentInterface
        => new GivenThatCommonContinuation<TSUT, TResult, TService, Continuations.Void, TMock>(
            _spec, new Lazy<TMock>(callback), _callExpr, callbackExpr);
}