using Moq;
using System.Linq.Expressions;
using XspecT.Internal.Pipelines;

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

    /// <summary>
    /// Return continuation for providing any setup as an action
    /// </summary>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
    public IGivenContinuation<TSUT, TResult> Given()
    {
        if (Pipeline.HasRun)
            throw new SetupFailed("Given must be called before Then");
        return new GivenContinuation<TSUT, TResult>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
    {
        Pipeline.Given(() => A(setup));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value)
    {
        Pipeline.Given(() => A(value, true));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
    {
        Pipeline.Given(() => A(value(), true));
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    internal IGivenSubjectTestPipeline<TSUT, TResult> GivenSetup(Action setup)
    {
        Pipeline.Given(setup);
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    internal void SetupMock<TService>(Action<Mock<TService>> setup) where TService : class
        => Pipeline.SetupMock(setup);

    internal void SetupMock<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, Func<TReturns> returns) where TService : class
        => Pipeline.SetupMock(expression, returns);

    internal void SetupMock<TService, TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression, Func<TReturns> returns) where TService : class
        => Pipeline.SetupMock(expression, returns);
}