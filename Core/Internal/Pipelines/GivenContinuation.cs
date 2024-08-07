using XspecT.Internal.TestData;

namespace XspecT.Internal.Pipelines;

internal class GivenContinuation<TSUT, TResult> : IGivenContinuation<TSUT, TResult> 
{
    private readonly Spec<TSUT, TResult> _spec;

    internal GivenContinuation(Spec<TSUT, TResult> spec) => _spec = spec;

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(TValue defaultValue)
        => _spec.Given(defaultValue, ApplyTo.Defaults);
 
    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(Func<TValue> defaultValue)
        => _spec.Given(defaultValue, ApplyTo.Defaults);

    public IGivenTestPipeline<TSUT, TResult> Using<TValue>(TValue defaultValue)
        => _spec.Given(defaultValue, ApplyTo.Mocker);
    
    public IGivenTestPipeline<TSUT, TResult> Using<TValue>(Func<TValue> defaultValue)
        => _spec.Given(defaultValue, ApplyTo.Mocker);

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(Action<TValue> defaultSetup)
         where TValue : class
        => _spec.Given(defaultSetup);

    public IGivenTestPipeline<TSUT, TResult> Default<TValue>(Func<TValue, TValue> defaultSetup)
        => _spec.Given(defaultSetup);

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(TValue value)
        => That(() => _spec.A(value));

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(Action<TValue> setup)
         where TValue : class
        => That(() => _spec.A(setup));

    public IGivenTestPipeline<TSUT, TResult> A<TValue>(Func<TValue, TValue> transform)
        => That(() => _spec.A(transform));

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(TValue value)
        => That(() => _spec.A(value));

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(Action<TValue> setup)
         where TValue : class
        => That(() => _spec.A(setup));

    public IGivenTestPipeline<TSUT, TResult> An<TValue>(Func<TValue, TValue> transform)
        => That(() => _spec.A(transform));

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(TValue value)
        => That(() => _spec.ASecond(value));

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(Action<TValue> setup)
         where TValue : class
        => That(() => _spec.ASecond(setup));

    public IGivenTestPipeline<TSUT, TResult> ASecond<TValue>(Func<TValue, TValue> transform)
        => That(() => _spec.ASecond(transform));

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(TValue value)
        => That(() => _spec.AThird(value));

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(Action<TValue> setup)
         where TValue : class
        => That(() => _spec.AThird(setup));

    public IGivenTestPipeline<TSUT, TResult> AThird<TValue>(Func<TValue, TValue> transform)
        => That(() => _spec.AThird(transform));

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(TValue value)
        => That(() => _spec.AFourth(value));

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(Action<TValue> setup)
         where TValue : class
        => That(() => _spec.AFourth(setup));

    public IGivenTestPipeline<TSUT, TResult> AFourth<TValue>(Func<TValue, TValue> transform)
        => That(() => _spec.AFourth(transform));

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(TValue value)
        => That(() => _spec.AFifth(value));

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(Action<TValue> setup)
         where TValue : class
        => That(() => _spec.AFifth(setup));

    public IGivenTestPipeline<TSUT, TResult> AFifth<TValue>(Func<TValue, TValue> transform)
        => That(() => _spec.AFifth(transform));

    public IGivenTestPipeline<TSUT, TResult> That(Action setup) => _spec.PushArrangement(setup);
}