namespace XspecT.Internal.Pipelines;

internal class GivenThatReturnsContinuation<TSUT, TResult, TService>
    : GivenTestPipeline<TSUT, TResult>, IGivenThatReturnsContinuation<TSUT, TResult, TService>
    where TService : class
{
    private readonly GivenServiceContinuation<TSUT, TResult, TService> _serviceContinuation;

    internal GivenThatReturnsContinuation(Spec<TSUT, TResult> spec) : base(spec) => _serviceContinuation = new(spec);

    public IGivenTestPipeline<TSUT, TResult> AndReturnsDefault<TReturns>(Func<TReturns> value)
        => _serviceContinuation.ReturnsDefault(value);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> AndThat<TReturns>(System.Linq.Expressions.Expression<Func<TService, TReturns>> expression)
        => _serviceContinuation.That(expression);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> AndThat<TReturns>(System.Linq.Expressions.Expression<Func<TService, Task<TReturns>>> expression)
        => _serviceContinuation.That(expression);
}