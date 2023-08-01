using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Fixture.Exceptions;
using XspecT.Fixture.Pipelines;
using XspecT.Verification;

using static XspecT.Internal.AsyncHelper;
namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override TestStatic or TestSubject instead
/// </summary>
public abstract class SpecBase<TResult> : Mocking, ITestPipeline<TResult>, IDisposable
{
    private readonly Stack<Action> _arrangements = new();
    private readonly List<Action> _substitutions = new();
    private Action _command;
    private Func<TResult> _function;
    private Exception _error;
    private TResult _result;
    private TestResult<TResult> _then;
    private object _arguments;

    public ITestPipeline<TValue, TResult> Given<TValue>(TValue value)
    {
        SetArguments(value);
        return new TestPipeline<TValue, TResult>(this);
    }

    public ITestPipeline<TValue1, TValue2, TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2)
    {
        SetArguments((value1, value2));
        return new TestPipeline<TValue1, TValue2, TResult>(this);
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> Given<TValue1, TValue2, TValue3>(
        TValue1 value1, TValue2 value2, TValue3 value3)
    {
        SetArguments((value1, value2, value3));
        return new TestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public ITestPipeline<TResult> GivenThat(Action arrangement)
    {
        if (_then != null)
            throw new SetupFailed("Given must be called before Then");
        _arrangements.Push(arrangement);
        return this;
    }

    public ITestPipeline<TResult> Using<TValue>([DisallowNull] Func<TValue> value)
        => Substitute(() => Mocker.Use(value()));

    public ITestPipeline<TResult> Using<TValue>([DisallowNull] TValue value)
        => Substitute(() => Mocker.Use(value));

    public ITestPipeline<TResult> Using<TValue1, TValue2>(
        [DisallowNull] TValue1 value1, [DisallowNull] TValue2 value2)
        => Substitute(() => Mocker.Use(value1), () => Mocker.Use(value2));

    public ITestPipeline<TResult> Using<TValue1, TValue2, TValue3>(
        [DisallowNull] TValue1 value1, [DisallowNull] TValue2 value2, [DisallowNull] TValue3 value3)
        => Substitute(() => Mocker.Use(value1), () => Mocker.Use(value2), () => Mocker.Use(value3));

    public ITestPipeline<TResult> Using(Type type, object value)
        => Substitute(() => Mocker.Use(type, value));

    public ITestPipeline<TResult> Using<TService>(Mock<TService> mockedService)
        where TService : class
        => Substitute(() => Mocker.Use(mockedService));

    public ITestPipeline<TResult> Using<TService>(Expression<Func<TService, bool>> setup)
        where TService : class
        => Substitute(() => Mocker.Use(setup));

    public TestResult<TResult> Then => _then ??= CreateTestResult();

    protected TResult Result => Then.Result;

    public ITestPipeline<TResult> When(Action act)
        => When(act ?? throw new SetupFailed("Act cannot be null"), null);

    public ITestPipeline<TValue, TResult> When<TValue>(Action<TValue> act)
    {
        When(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            act(arg);
        });
        return new TestPipeline<TValue, TResult>(this);
    }

    public ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Action<TValue1, TValue2> act)
    {
        When(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            act(arg1, arg2);
        });
        return new TestPipeline<TValue1, TValue2, TResult>(this);
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act)
    {
        When(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            act(arg1, arg2, arg3);
        });
        return new TestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public ITestPipeline<TResult> When(Func<TResult> act)
        => When(null, act ?? throw new SetupFailed("Act cannot be null"));

    public ITestPipeline<TValue, TResult> When<TValue>(Func<TValue, TResult> act)
    {
        When(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            return act(arg);
        });
        return new TestPipeline<TValue, TResult>(this);
    }

    public ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act)
    {
        When(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            return act(arg1, arg2);
        });
        return new TestPipeline<TValue1, TValue2, TResult>(this);
    }

    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, TResult> act)
    {
        When(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            return act(arg1, arg2, arg3);
        });
        return new TestPipeline<TValue1, TValue2, TValue3, TResult>(this);
    }

    public ITestPipeline<TResult> When(Func<Task> action) => When(() => Execute(action));
    public ITestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task> action)
        => When<TValue>(v => Execute(() => action(v)));
    public ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task> action)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => action(v1, v2)));
    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task> action)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => action(v1, v2, v3)));

    public ITestPipeline<TResult> When(Func<Task<TResult>> func) => When(() => Execute(func));
    public ITestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task<TResult>> func)
        => When<TValue>(v => Execute(() => func(v)));
    public ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task<TResult>> func)
        => When<TValue1, TValue2>((v1, v2) => Execute(() => func(v1, v2)));
    public ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, Task<TResult>> func)
        => When<TValue1, TValue2, TValue3>((v1, v2, v3) => Execute(() => func(v1, v2, v3)));

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

    protected virtual void TearDown() { }
    protected virtual Task TearDownAsync() => Task.CompletedTask;

    private ITestPipeline<TResult> Substitute(params Action[] substitutions)
    {
        if (_then != null)
            throw new SetupFailed("Use must be called before Then");
        foreach (var arrange in substitutions)
            _substitutions.Add(arrange);
        return this;
    }

    private void SetArguments(object args)
    {
        if (_then != null)
            throw new SetupFailed("Given must be called before Then");
        if (_arguments is not null)
            throw new SetupFailed("Can only supply method arguments once");
        _arguments = args;
    }

    private ITestPipeline<TResult> When(Action command, Func<TResult> function)
    {
        if (_command != null || _function != null)
            throw new SetupFailed("When may only be called once");
        if (_then != null)
            throw new SetupFailed("When must be called before Then");
        (_command, _function) = (command, function);
        return this;
    }

    private TestResult<TResult> CreateTestResult()
    {
        Set();
        foreach (var arrange in _arrangements) arrange();
        foreach (var substitute in _substitutions) substitute();
        Setup();
        Instantiate();
        CatchError(_command ?? GetResult);
        return new(_result, _error, this);
    }

    private void GetResult() => _result = _function();

    private void CatchError(Action act)
    {
        try
        {
            act();
        }
        catch (SetupFailed)
        {
            throw;
        }
        catch (Exception ex)
        {
            _error = ex;
        }
    }
}