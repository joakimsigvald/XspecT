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

    public IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class
        => Parent.Given<TService>();

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
        => Parent.Given(setup);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
        => Parent.Given(value);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value) => Parent.Given(value);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2)
        => Parent.Given(value1, value2);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2, TValue value3)
        => Parent.Given(value1, value2, value3);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2, TValue value3, TValue value4)
        => Parent.Given(value1, value2, value3, value4);

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2, TValue value3, TValue value4, TValue value5)
        => Parent.Given(value1, value2, value3, value4, value5);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class
        => Given(setup);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value) => Given(value);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value) => Given(value);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value1, TValue value2)
        => Given(value1, value2);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value1, TValue value2, TValue value3)
        => Given(value1, value2, value3);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value1, TValue value2, TValue value3, TValue value4)
        => Given(value1, value2, value3, value4);

    public IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value1, TValue value2, TValue value3, TValue value4, TValue value5)
        => Given(value1, value2, value3, value4, value5);

    public IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class
        => Given<TService>();
}