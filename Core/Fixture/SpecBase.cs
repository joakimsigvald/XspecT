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

    public ITestPipeline<TResult> Using<TValue>([DisallowNull] Func<TValue> value)
        => Using(() => Use(value()));

    public ITestPipeline<TResult> Using<TValue>(TValue value)
        => Using(() => Use(value));

    public ITestPipeline<TResult> Using<TValue1, TValue2>(
        TValue1 value1, TValue2 value2)
        => Using(() => Use(value1), () => Use(value2));

    public ITestPipeline<TResult> Using<TValue1, TValue2, TValue3>(
        TValue1 value1, TValue2 value2, TValue3 value3)
        => Using(() => Use(value1), () => Use(value2), () => Use(value3));

    public ITestPipeline<TResult> Using<TService>(Mock<TService> mockedService)
        where TService : class
        => Using(() => Use(mockedService));

    public ITestPipeline<TResult> Using<TService>(Expression<Func<TService, bool>> setup)
        where TService : class
        => Using(() => Use(setup));

    public ITestPipeline<TResult> When(Action act)
        => When(act ?? throw new SetupFailed("Act cannot be null"), null);

    public ITestPipeline<TResult> When(Func<TResult> act)
        => When(null, act ?? throw new SetupFailed("Act cannot be null"));

    public ITestPipeline<TResult> When(Func<Task> action) => When(() => Execute(action));

    public ITestPipeline<TResult> When(Func<Task<TResult>> func) => When(() => Execute(func));

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

    protected TSpec Then<TSpec>(TSpec spec)
    {
        Then();
        return spec;
    }

    protected TResult Result => Then().Result;

    protected virtual void TearDown() { }
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