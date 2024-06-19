using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenServiceContinuation<TSUT, TResult, TService> : IGivenServiceContinuation<TSUT, TResult, TService>
    where TService : class
{
    private readonly Spec<TSUT, TResult> _spec;

    internal GivenServiceContinuation(Spec<TSUT, TResult> spec) => _spec = spec;

    public IGivenTestPipeline<TSUT, TResult> ReturnsDefault<TReturns>(Func<TReturns> value)
    {
        _spec.GivenSetup(() => _spec.GetMock<TService>().SetReturnsDefault(value()));
        return new GivenTestPipeline<TSUT, TResult>(_spec);
    }

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns, TReturns>(_spec, expression);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns, Task<TReturns>>(_spec, expression);
}