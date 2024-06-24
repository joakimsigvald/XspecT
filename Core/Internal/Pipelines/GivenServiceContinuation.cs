using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenServiceContinuation<TSUT, TResult, TService> : IGivenServiceContinuation<TSUT, TResult, TService>
    where TService : class
{
    private readonly Spec<TSUT, TResult> _spec;

    internal GivenServiceContinuation(Spec<TSUT, TResult> spec) => _spec = spec;

    public IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TReturns>(Func<TReturns> value)
    {
        var theValue = value();
        _spec.GivenSetup(() => _spec.GetMock<TService>().SetReturnsDefault(theValue));
        _spec.GivenSetup(() => _spec.GetMock<TService>().SetReturnsDefault(Task.FromResult(theValue)));
        return new GivenThatReturnsContinuation<TSUT, TResult, TService>(_spec);
    }

    public IGivenTestPipeline<TSUT, TResult> ReturnsDefault<TReturns>(Func<TReturns> value)
    {
        var theValue = value();
        _spec.GivenSetup(() => _spec.GetMock<TService>().SetReturnsDefault(theValue));
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns, TReturns>(
            _spec, new Lazy<Moq.Language.Flow.ISetup<TService, TReturns>>(() => _spec.GetMock<TService>().Setup(expression)));

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns, Task<TReturns>>(
            _spec, new Lazy<Moq.Language.Flow.ISetup<TService, Task<TReturns>>>(() => _spec.GetMock<TService>().Setup(expression)));
}