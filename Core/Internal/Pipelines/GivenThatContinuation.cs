using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenThatContinuation<TSUT, TResult, TService, TReturns, TActualReturns>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    private readonly Spec<TSUT, TResult> _spec;
    private readonly Moq.Language.Flow.ISetup<TService, TActualReturns> _setup;

    internal GivenThatContinuation(
        Spec<TSUT, TResult> spec, Expression<Func<TService, TActualReturns>> expression)
    {
        _spec = spec;
        _setup = _spec.GetMock<TService>().Setup(expression);
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

    private void DoSetupReturns(Func<TReturns> returns)
    {
        if (typeof(TActualReturns) == typeof(Task<TReturns>))
        {
            var asyncSetup = (Moq.Language.Flow.ISetup<TService, Task<TReturns>>)_setup;
            asyncSetup.ReturnsAsync(returns);
        }
        else _setup.Returns(returns);
    }
}