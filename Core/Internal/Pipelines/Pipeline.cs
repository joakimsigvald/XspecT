using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Fixture;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;
using XspecT.Verification;

using static XspecT.Internal.Pipelines.AsyncHelper;

namespace XspecT.Internal.Pipelines;

internal class Pipeline<TResult> : IPipeline<TResult>
{
    protected readonly Context _context = new();
    private readonly SpecActor<TResult> _actor = new();
    private TestResult<TResult> _then;

    public bool HasRun => _then != null;

    public ITestResult<TResult> Then() => TestResult;

    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression) where TService : class
        => TestResult.Verify(expression);

    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times) where TService : class
        => TestResult.Verify(expression, times);

    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class
        => TestResult.Verify(expression, times);

    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class
        => TestResult.Verify(expression);

    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class
        => TestResult.Verify(expression, times);

    public IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class
        => TestResult.Verify(expression, times);

    public void SetAction(Action act)
    {
        if (HasRun)
            throw new SetupFailed("When must be called before Then");
        _actor.When(act ?? throw new SetupFailed("Act cannot be null"));
    }

    public void SetAction(Func<TResult> act)
    {
        if (HasRun)
            throw new SetupFailed("When must be called before Then");
        _actor.When(act ?? throw new SetupFailed("Act cannot be null"));
    }

    public void SetAction(Func<Task> action) => SetAction(() => Execute(action));

    public void SetAction(Func<Task<TResult>> func) => SetAction(() => Execute(func));

    protected virtual void Arrange() { }

    public TValue Mention<TValue>(int index) => _context.Mention<TValue>(index);

    public TValue Create<TValue>() => _context.Create<TValue>();

    public TValue Create<TValue>([NotNull] Action<TValue> setup)
    {
        if (HasRun)
            throw new SetupFailed("Setup to auto-generated values must be provided before Then");
        return Context.ApplyTo(setup, _context.Create<TValue>());
    }

    public TValue[] MentionMany<TValue>(int count) => _context.MentionMany<TValue>(count);

    public TValue Mention<TValue>(string label) => _context.Mention<TValue>(label);

    public TValue[] MentionMany<TValue>([NotNull] Action<TValue> setup, int count)
        => _context.MentionMany(setup, count);

    public TValue Mention<TValue>(int index, [NotNull] Action<TValue> setup)
    {
        if (HasRun)
            throw new SetupFailed("Setup to auto-generated values must be provided before Then");
        return _context.Mention(index, setup);
    }

    public TValue Mention<TValue>(int index, TValue value, bool asDefault = false)
    {
        if (HasRun)
            throw new SetupFailed("Setup to auto-generated values must be provided before Then");
        return _context.Mention(value, index, asDefault);
    }

    public TValue[] MentionMany<TValue>([NotNull] Action<TValue, int> setup, int count)
        => _context.MentionMany(setup, count);

    private TestResult<TResult> TestResult => _then ??= Run();

    private TestResult<TResult> Run()
    {
        Arrange();
        return _actor.Execute(_context);
    }
}