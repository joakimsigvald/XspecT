using Moq;
using System.Globalization;
using System.Linq.Expressions;
using XspecT.Fixture.Pipelines;
using XspecT.Verification;

using static XspecT.Internal.AsyncHelper;

namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override TestStatic or TestSubject instead
/// </summary>
public abstract partial class Spec<TResult> : ITestPipeline<TResult>, IDisposable
{
    internal protected readonly IPipeline<TResult> _pipeline;

    protected Spec(IPipeline<TResult> pipeline)
    {
        CultureInfo.CurrentCulture = GetCulture();
        _pipeline = pipeline;
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

    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression) where TService : class
        => _pipeline.Then(expression);

    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times) where TService : class
        => _pipeline.Then(expression, times);

    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class
        => _pipeline.Then(expression, times);

    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class
        => _pipeline.Then(expression);

    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class
        => _pipeline.Then(expression, times);

    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class
        => _pipeline.Then(expression, times);

    public void Dispose()
    {
        TearDown();
        Execute(TearDownAsync);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Contains the returned value after calling method-under-test
    /// </summary>
    protected TResult Result => _pipeline.Then().Result;

    /// <summary>
    /// Override this method to provide tear-down logic after test has run
    /// </summary>
    protected virtual void TearDown() { }

    /// <summary>
    /// Override this method to provide async tear-down logic after test has run
    /// </summary>
    protected virtual Task TearDownAsync() => Task.CompletedTask;

    /// <summary>
    /// Override this to set different Culture than InvariantCulture during test
    /// </summary>
    /// <returns></returns>
    protected virtual CultureInfo GetCulture() => CultureInfo.InvariantCulture;
}