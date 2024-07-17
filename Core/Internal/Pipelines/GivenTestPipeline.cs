using System.Linq.Expressions;

namespace XspecT.Internal.Pipelines;

internal class GivenTestPipeline<TSUT, TResult>
    : TestPipeline<TSUT, TResult, Spec<TSUT, TResult>>, IGivenTestPipeline<TSUT, TResult>
{
    internal GivenTestPipeline(Spec<TSUT, TResult> parent) : base(parent) { }

    public ITestPipeline<TSUT, TResult> When(Expression<Action<TSUT>> act)
        => Parent.When(act);

    public ITestPipeline<TSUT, TResult> When(Expression<Func<TSUT, TResult>> act)
        => Parent.When(act);

    public ITestPipeline<TSUT, TResult> When(Expression<Func<TSUT, Task>> action)
        => Parent.When(action);

    public ITestPipeline<TSUT, TResult> When(Expression<Func<TSUT, Task<TResult>>> func)
        => Parent.When(func);

    public ITestPipeline<TSUT, TResult> After(Action<TSUT> setUp)
        => Parent.After(setUp);

    public ITestPipeline<TSUT, TResult> After(Func<TSUT, Task> setUp)
        => Parent.After(setUp);

    public ITestPipeline<TSUT, TResult> Before(Action<TSUT> tearDown)
        => Parent.Before(tearDown);

    public ITestPipeline<TSUT, TResult> Before(Func<TSUT, Task> tearDown)
        => Parent.Before(tearDown);

    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
        => Parent.Given(setup);
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue, TValue> setup)
        => Parent.Given(setup);

    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(TValue defaultValue)
        => Parent.Given(defaultValue);

    public IGivenServiceContinuation<TSUT, TResult, TService> Given<TService>() where TService : class
        => Parent.Given<TService>();

    public IGivenContinuation<TSUT, TResult> Given() => Parent.Given();

    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value) => Parent.Given(value);
    public IGivenTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class
        => Given(setup);
    public IGivenTestPipeline<TSUT, TResult> And<TValue>(Func<TValue, TValue> setup)
        => Given(setup);

    public IGivenTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value) => Given(value);
    public IGivenTestPipeline<TSUT, TResult> And<TValue>(TValue value) => Given(value);
    public IGivenServiceContinuation<TSUT, TResult, TService> And<TService>() where TService : class => Given<TService>();
    public IGivenContinuation<TSUT, TResult> And() => Given();
}