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
    private readonly SpecActor<TResult> _actor = new ();
    private TestResult<TResult> _then;
    protected bool HasRun => _then != null;

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

    protected internal void SetAction(Action act)
        => SetAction(act ?? throw new SetupFailed("Act cannot be null"), null);

    protected internal void SetAction(Func<TResult> act)
        => SetAction(null, act ?? throw new SetupFailed("Act cannot be null"));

    protected internal void SetAction(Func<Task> action) => SetAction(() => Execute(action));

    protected internal void SetAction(Func<Task<TResult>> func) => SetAction(() => Execute(func));

    private ITestPipeline<TResult> SetAction(Action command, Func<TResult> function)
    {
        if (HasRun)
            throw new SetupFailed("When must be called before Then");
        _actor.When(command, function);
        return this;
    }

    private TestResult<TResult> Run()
    {
        Set();
        Arrange();
        return _actor.Execute(this);
    }

    /// <summary>
    /// Not intended to override or call
    /// </summary>
    protected internal virtual void Arrange() { }
}