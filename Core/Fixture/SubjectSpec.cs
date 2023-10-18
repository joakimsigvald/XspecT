using Moq;
using System.Linq.Expressions;
using XspecT.Fixture.Exceptions;
using XspecT.Fixture.Pipelines;
using XspecT.Internal;

namespace XspecT.Fixture;

public abstract class SubjectSpec<TSUT, TResult> : Spec<TResult>, ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    protected SubjectSpec() : base(new SubjectPipeline<TSUT, TResult>()) { }
    private SubjectPipeline<TSUT, TResult> Pipeline => (SubjectPipeline<TSUT, TResult>)_pipeline;
    protected TSUT SUT => Pipeline.SUT;

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act)
    {
        Pipeline.SetAction(() => act(SUT));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
    {
        Pipeline.SetAction(() => act(SUT));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
    {
        Pipeline.SetAction(() => action(SUT));
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
    {
        Pipeline.SetAction(() => func(SUT));
        return this;
    }

    public IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class
    {
        if (Pipeline.HasRun)
            throw new SetupFailed("Given must be called before Then");
        return new GivenContinuation<TSUT, TResult, TService>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
    {
        Pipeline.Given(() => A(setup));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
    {
        Pipeline.Given(() => A(value()));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value)
    {
        Pipeline.Given(() => A(value));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2) 
        => Given(value1).And(() => ASecond(value2));

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2, TValue value3)
        => Given(value1, value2).And(() => AThird(value3));

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2, TValue value3, TValue value4)
        => Given(value1, value2, value3).And(() => AFourth(value4));

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value1, TValue value2, TValue value3, TValue value4, TValue value5)
        => Given(value1, value2, value3, value4).And(() => AFifth(value5));

    internal void SetupMock<TService>(Action<Mock<TService>> setup) where TService : class
        => Pipeline.SetupMock(setup);

    internal void SetupMock<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, Func<TReturns> returns) where TService : class
        => Pipeline.SetupMock(expression, returns);

    internal void SetupMock<TService, TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression, Func<TReturns> returns) where TService : class
        => Pipeline.SetupMock(expression, returns);
}