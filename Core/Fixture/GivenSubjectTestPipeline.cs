using Moq;
using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

public class GivenSubjectTestPipeline<TSUT, TResult>
    : TestPipeline<TResult, SubjectSpec<TSUT, TResult>>, IGivenSubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    public GivenSubjectTestPipeline(SubjectSpec<TSUT, TResult> parent)
        : base(parent) { }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
        => Parent.When(act);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
        => Parent.When(action);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
        => Parent.When(func);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class
        => Given(setup);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value) => Given(value);
    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value)
    {
        Parent.Given(value);
        return this;
    }

    public IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class
        => Parent.Given<TService>();

    public IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class
        => Given<TService>();

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
    {
        Parent.Given(setup);
        return this;
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
    {
        Parent.Given(value);
        return this;
    }
}