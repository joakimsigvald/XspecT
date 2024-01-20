using Moq;
using System.Linq.Expressions;
using XspecT.Internal.Pipelines;

namespace XspecT;

/// <summary>
/// Base-class for specifying and executing a set of test cases for a specific method-under-test on a class instance
/// </summary>
/// <typeparam name="TSUT">The class to instantiate and execute the method-under-test on</typeparam>
/// <typeparam name="TResult">The return type of the method-under-test</typeparam>
public abstract class SubjectSpec<TSUT, TResult> : Spec<TResult>, ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    /// <summary>
    /// Instantiate the specification (test-class)
    /// </summary>
    protected SubjectSpec() : base(new SubjectPipeline<TSUT, TResult>()) { }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act)
    {
        Pipeline.SetAction(act);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
    {
        Pipeline.SetAction(act);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
    {
        Pipeline.SetAction(action);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
    {
        Pipeline.SetAction(func);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Before(Action<TSUT> tearDown)
    {
        Pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> Before(Func<TSUT, Task> tearDown)
    {
        Pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> After(Action<TSUT> setUp)
    {
        Pipeline.SetSetUp(setUp);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ISubjectTestPipeline<TSUT, TResult> After(Func<TSUT, Task> setUp)
    {
        Pipeline.SetSetUp(setUp);
        return this;
    }

    /// <summary>
    /// Provide any arrangement to the test, which will be applied during test execution in reverse order of where in the test-pipleine it was provided
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
    {
        Pipeline.SetDefault(setup);
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Provide a default value, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue defaultValue)
    {
        Pipeline.SetDefault(defaultValue);
        return new GivenSubjectTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// A continuation for providing mock-setup for the given type
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    /// <exception cref="SetupFailed"></exception>
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

    /// <summary>
    /// Provide a default value as a lambda, to be evaluated during test execution AFTER any subsequently added arrangement.
    /// Providing a default value as a lambda, to defer execution, is useful when the default value is created based on test data that is specified later in the test-pipeline.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
    {
        Pipeline.Given(() => ADefault(value()));
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

    private TValue ADefault<TValue>(TValue value) => _pipeline.Mention(0, value, true);

    private SubjectPipeline<TSUT, TResult> Pipeline => (SubjectPipeline<TSUT, TResult>)_pipeline;
}