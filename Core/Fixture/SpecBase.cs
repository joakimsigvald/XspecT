using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Verification;

namespace XspecT.Fixture;

/// <summary>
/// Not intended for direct override. Override one of TestStatic, TestSubject, TestStaticAsync or TestSubjectAsync instead
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

    public ITestPipeline<TResult> Given<TValue>(TValue value)
        => SetArguments(value);

    public ITestPipeline<TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2)
        => SetArguments((value1, value2));

    public ITestPipeline<TResult> Given<TValue1, TValue2, TValue3>(TValue1 value1, TValue2 value2, TValue3 value3)
        => SetArguments((value1, value2, value3));

    private ITestPipeline<TResult> SetArguments(object args)
    {
        if (_then != null)
            throw new InvalidOperationException("Given must be called before Then");
        if (_arguments is not null)
            throw new InvalidOperationException("Can only supply method arguments once");
        _arguments = args;
        return this;
    }

    public ITestPipeline<TResult> Given(params Action[] arrangements)
    {
        if (_then != null)
            throw new InvalidOperationException("Given must be called before Then");
        foreach (var arrange in arrangements.Reverse())
            _arrangements.Push(arrange);
        return this;
    }

    public ITestPipeline<TResult> Substitute(params Action[] substitutions)
    {
        if (_then != null)
            throw new InvalidOperationException("Use must be called before Then");
        foreach (var arrange in substitutions)
            _substitutions.Add(arrange);
        return this;
    }

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

    public abstract void Dispose();

    protected TResult Result => Then.Result;

    protected ITestPipeline<TResult> When(Action act)
        => When(act ?? throw new InvalidOperationException("Act cannot be null"), null);

    protected ITestPipeline<TResult> When(Func<TResult> act)
        => When(null, act ?? throw new InvalidOperationException("Act cannot be null"));

    protected ITestPipeline<TResult> When<TValue>(Action<TValue> act)
        => When(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            act(arg);
        });

    protected ITestPipeline<TResult> When<TValue>(Func<TValue, TResult> act)
        => When(() =>
        {
            var arg = _arguments is TValue val ? val : default;
            return act(arg);
        });

    protected ITestPipeline<TResult> When<TValue1, TValue2>(Action<TValue1, TValue2> act)
        => When(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            act(arg1, arg2);
        });

    protected ITestPipeline<TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act)
        => When(() =>
        {
            var (arg1, arg2) = _arguments is ValueTuple<TValue1, TValue2> t ? t : default;
            return act(arg1, arg2);
        });

    protected ITestPipeline<TResult> When<TValue1, TValue2, TValue3>(Action<TValue1, TValue2, TValue3> act)
        => When(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            act(arg1, arg2, arg3);
        });

    protected ITestPipeline<TResult> When<TValue1, TValue2, TValue3>(Func<TValue1, TValue2, TValue3, TResult> act)
        => When(() =>
        {
            var (arg1, arg2, arg3) = _arguments is ValueTuple<TValue1, TValue2, TValue3> t ? t : default;
            return act(arg1, arg2, arg3);
        });

    protected ITestPipeline<TResult> When(Action command, Func<TResult> function)
    {
        if (_command != null || _function != null)
            throw new InvalidOperationException("When may only be called once");
        if (_then != null)
            throw new InvalidOperationException("When must be called before Then");
        (_command, _function) = (command, function);
        return this;
    }

    protected virtual void Set() { }

    protected virtual void Setup() { }

    protected abstract void Instantiate();

    private TestResult<TResult> CreateTestResult()
    {
        foreach (var substitute in _substitutions) substitute();
        Set();
        foreach (var arrange in _arrangements) arrange();
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
        catch (Exception ex)
        {
            _error = ex;
        }
    }
}