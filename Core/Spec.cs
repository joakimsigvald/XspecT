using Moq;
using System.Globalization;
using System.Linq.Expressions;
using XspecT.Internal.Pipelines;

namespace XspecT;

[Obsolete("Replaced by Spec")]
public abstract class SubjectSpec<TSUT, TResult> : Spec<TSUT, TResult>
{
}

[Obsolete("Replaced by Spec")]
public abstract class StaticSpec<TResult> : Spec<object, TResult>
{
}

public abstract class Spec<TSUTandResult> : Spec<TSUTandResult, TSUTandResult>
{
}

/// <summary>
/// Base-class for specifying and executing a set of test cases for a specific method-under-test
/// </summary>
/// <typeparam name="TSUT">The class to instantiate and execute the method-under-test on, use object for static method</typeparam>
/// <typeparam name="TResult">The return type of the method-under-test</typeparam>
public abstract partial class Spec<TSUT, TResult> : ITestPipeline<TSUT, TResult>
{
    private readonly Pipeline<TSUT, TResult> _pipeline;

    /// <summary>
    /// 
    /// </summary>
    protected Spec()
    {
        CultureInfo.CurrentCulture = GetCulture();
        _pipeline = new Pipeline<TSUT, TResult>();
    }

    /// <summary>
    /// Run the test-pipeline and return the result
    /// </summary>
    /// <returns>The test result</returns>
    public ITestResult<TResult> Then() => _pipeline.Then();

    /// <summary>
    /// Run the test-pipeline and return a given subject to be used in chained assertions.
    /// </summary>
    /// <typeparam name="TSubject"></typeparam>
    /// <param name="subject"></param>
    /// <returns>the given subject</returns>
    public TSubject Then<TSubject>(TSubject subject)
    {
        _pipeline.Then();
        return subject;
    }

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression) where TService : class
        => _pipeline.Then(expression);

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times) where TService : class
        => _pipeline.Then(expression, times);

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class
        => _pipeline.Then(expression, times);

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class
        => _pipeline.Then(expression);

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class
        => _pipeline.Then(expression, times);

    /// <summary>
    /// Run the test-pipeline and verify mock invocation.
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class
        => _pipeline.Then(expression, times);

    /// <summary>
    /// Contains the returned value after calling method-under-test
    /// </summary>
    protected TResult Result => _pipeline.Then().Result;

    /// <summary>
    /// Override this to set different Culture than InvariantCulture during test
    /// </summary>
    /// <returns></returns>
    protected virtual CultureInfo GetCulture() => CultureInfo.InvariantCulture;

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Action<TSUT> act)
    {
        Pipeline.SetAction(act);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act)
    {
        Pipeline.SetAction(act);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, Task> action)
    {
        Pipeline.SetAction(action);
        return this;
    }

    /// <summary>
    /// Provide the method-under-test to the test-pipeline
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func)
    {
        Pipeline.SetAction(func);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(Action<TSUT> tearDown)
    {
        Pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the tearDown to the test-pipeline
    /// </summary>
    /// <param name="tearDown"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> Before(Func<TSUT, Task> tearDown)
    {
        Pipeline.SetTearDown(tearDown);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(Action<TSUT> setUp)
    {
        Pipeline.PrependSetUp(setUp);
        return this;
    }

    /// <summary>
    /// Provide the setUp to the test-pipeline
    /// </summary>
    /// <param name="setUp"></param>
    /// <returns></returns>
    public ITestPipeline<TSUT, TResult> After(Func<TSUT, Task> setUp)
    {
        Pipeline.PrependSetUp(setUp);
        return this;
    }

    /// <summary>
    /// Provide any arrangement to the test, which will be applied during test execution in reverse order of where in the test-pipleine it was provided
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class
    {
        Pipeline.SetDefault(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Transform any value and use the transformed value as default
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue, TValue> setup)
    {
        Pipeline.SetDefault(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    /// <summary>
    /// Provide a default value, that will be applied in all mocks and auto-generated test-data, where no specific value or setup is given.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(TValue defaultValue)
    {
        Pipeline.SetDefault(defaultValue);
        return new GivenTestPipeline<TSUT, TResult>(this);
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
    public IGivenTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value)
    {
        Pipeline.Given(() => ADefault(value()));
        return new GivenTestPipeline<TSUT, TResult>(this);
    }

    internal IGivenTestPipeline<TSUT, TResult> GivenSetup(Action setup)
    {
        Pipeline.Given(setup);
        return new GivenTestPipeline<TSUT, TResult>(this);
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

    private Pipeline<TSUT, TResult> Pipeline => _pipeline;
}