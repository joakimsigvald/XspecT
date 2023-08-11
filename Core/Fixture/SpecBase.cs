using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Fixture.Exceptions;
using XspecT.Fixture.Pipelines;
using XspecT.Internal;
using XspecT.Verification;

using static XspecT.Internal.AsyncHelper;
namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override TestStatic or TestSubject instead
/// </summary>
public abstract class SpecBase<TResult> : Mocking, ITestPipeline<TResult>, IDisposable
{
    private readonly Stack<Action> _arrangements = new();
    private readonly List<Action> _usings = new();
    private readonly SpecActor<TResult> _actor = new ();
    private TestResult<TResult> _then;
    protected bool HasRun => _then != null;

    public ITestPipeline<TResult> GivenThat(Action arrangement)
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        _arrangements.Push(arrangement);
        return this;
    }

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> Using<TService>([DisallowNull] Func<TService> service)
        => Using(() => Use(service()));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> Using<TService>(TService service)
        => Using(() => Use(service));

    /// <summary>
    /// Provide services to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService1"></typeparam>
    /// <typeparam name="TService2"></typeparam>
    /// <param name="service1"></param>
    /// <param name="service2"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> Using<TService1, TService2>(
        TService1 service1, TService2 service2)
        => Using(() => Use(service1), () => Use(service2));

    /// <summary>
    /// Provide services to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService1"></typeparam>
    /// <typeparam name="TService2"></typeparam>
    /// <typeparam name="TService3"></typeparam>
    /// <param name="service1"></param>
    /// <param name="service2"></param>
    /// <param name="service3"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> Using<TService1, TService2, TService3>(
        TService1 service1, TService2 service2, TService3 service3)
        => Using(() => Use(service1), () => Use(service2), () => Use(service3));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="mockedService"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> Using<TService>(Mock<TService> mockedService)
        where TService : class
        => Using(() => Use(mockedService));

    /// <summary>
    /// Provide service to the test-pipeline that can be used in auto-mocking
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> Using<TService>(Expression<Func<TService, bool>> setup)
        where TService : class
        => Using(() => Use(setup));

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When(Action act)
        => When(act ?? throw new SetupFailed("Act cannot be null"), null);

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When(Func<TResult> act)
        => When(null, act ?? throw new SetupFailed("Act cannot be null"));

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When(Func<Task> action) => When(() => Execute(action));

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ITestPipeline<TResult> When(Func<Task<TResult>> func) => When(() => Execute(func));

    /// <summary>
    /// Run the test pipeline, before accessing the result
    /// </summary>
    /// <returns>The test result</returns>
    public TestResult<TResult> Then() => _then ??= Run();

    public void Dispose()
    {
        TearDown();
        Execute(TearDownAsync);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Convenience method for assigning fields in the test class that is used in later test setup.
    /// Will be called during pipeline execution right before the first arrangement
    /// </summary>
    protected virtual void Set() { }

    /// <summary>
    /// Convenience method for supplying setup (typically specifiing behaviour of mocks 
    /// that will be used and or verified during the test execution)
    /// Will be called during pipeline execution right after the last arrangement
    /// </summary>
    protected virtual void Setup() { }

    protected abstract void Instantiate();

    /// <summary>
    /// Run the test-pipeline and return the test-class (specification).
    /// Use this method to access any member on the testclass after the test is run, for a more fluent experience
    /// </summary>
    /// <typeparam name="TSpec"></typeparam>
    /// <param name="spec"></param>
    /// <returns></returns>
    protected TSpec Then<TSpec>(TSpec spec)
    {
        Then();
        return spec;
    }

    /// <summary>
    /// Contains the returned value after calling method-under-test
    /// </summary>
    protected TResult Result => Then().Result;

    /// <summary>
    /// Override this method to provide tear-down logic after test has run
    /// </summary>
    protected virtual void TearDown() { }

    /// <summary>
    /// Override this method to provide async tear-down logic after test has run
    /// </summary>
    protected virtual Task TearDownAsync() => Task.CompletedTask;

    private ITestPipeline<TResult> Using(params Action[] usings)
    {
        if (HasRun)
            throw new SetupFailed("Use must be called before Then");
        foreach (var use in usings)
            _usings.Add(use);
        return this;
    }

    private ITestPipeline<TResult> When(Action command, Func<TResult> function)
    {
        if (HasRun)
            throw new SetupFailed("When must be called before Then");
        _actor.When(command, function);
        return this;
    }

    private TestResult<TResult> Run()
    {
        Arrange();
        return _actor.Execute(this);
    }

    private void Arrange()
    {
        Set();
        foreach (var arrange in _arrangements) arrange();
        foreach (var use in _usings) use();
        Setup();
        Instantiate();
    }
}