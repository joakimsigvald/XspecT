using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenContinuation<TSUT, TResult, TService> : IGivenContinuation<TSUT, TResult, TService>
    where TService : class
{
    private readonly Spec<TSUT, TResult> _subjectSpec;

    internal GivenContinuation(Spec<TSUT, TResult> subjectSpec) 
        => _subjectSpec = subjectSpec;

    public IGivenTestPipeline<TSUT, TResult> ReturnsDefault<TReturns>(Func<TReturns> value)
    {
        _subjectSpec.SetupMock<TService>(mock => mock.SetReturnsDefault(value()));
        return new GivenTestPipeline<TSUT, TResult>(_subjectSpec);
    }

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression)
        => new GivenThatContinuation<TSUT, TResult, TService, TReturns>(_subjectSpec, expression);

    public IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression)
        => new GivenThatAsyncContinuation<TSUT, TResult, TService, TReturns>(_subjectSpec, expression);
}

internal class GivenContinuation<TSUT, TResult> : IGivenContinuation<TSUT, TResult> 
{
    private readonly Spec<TSUT, TResult> _spec;

    internal GivenContinuation(Spec<TSUT, TResult> spec) => _spec = spec;

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(TValue defaultValue)
        => _spec.Given(defaultValue);

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(Action<TValue> defaultSetup)
         where TValue : class
        => _spec.Given(defaultSetup);

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(Func<TValue, TValue> defaultSetup)
        => _spec.Given(defaultSetup);

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(TValue value)
        => That(() => _spec.A(value));

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(TValue value)
        => That(() => _spec.ASecond(value));

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(TValue value)
        => That(() => _spec.AThird(value));

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(TValue value)
        => That(() => _spec.AFourth(value));

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(TValue value)
        => That(() => _spec.AFifth(value));

    public IGivenTestPipeline<TSUT, TResult> That(Action setup) => _spec.GivenSetup(setup);
}