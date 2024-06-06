using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

using static XspecT.Internal.Pipelines.AsyncHelper;

namespace XspecT.Internal.Pipelines;

internal class Pipeline<TResult>
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

    internal void SetDefault<TModel>(Action<TModel> setup) where TModel : class
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        _context.SetDefault(setup);
    }

    internal void SetDefault<TValue>(Func<TValue, TValue> setup)
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        _context.SetDefault(setup);
    }

    internal void SetDefault<TValue>(TValue defaultValue)
    {
        if (HasRun)
            throw new SetupFailed("Given must be called before Then");
        _context.Use(defaultValue);
    }

    internal void SetAction(Action act)
    {
        if (HasRun)
            throw new SetupFailed("When must be called before Then");
        _actor.When(act ?? throw new SetupFailed("Act cannot be null"));
    }

    internal void SetAction(Func<TResult> act)
    {
        if (HasRun)
            throw new SetupFailed("When must be called before Then");
        _actor.When(act ?? throw new SetupFailed("Act cannot be null"));
    }

    internal void SetAction(Func<Task> action) => SetAction(() => Execute(action));

    internal void SetAction(Func<Task<TResult>> func) => SetAction(() => Execute(func));

    internal void PrependSetUp(Action setUp)
    {
        if (HasRun)
            throw new SetupFailed("After must be called before Then");
        _actor.After(setUp ?? throw new SetupFailed("SetUp cannot be null"));
    }

    internal void PrependSetUp(Func<Task> setUp) => PrependSetUp(() => Execute(setUp));

    internal void SetTearDown(Action tearDown)
    {
        if (HasRun)
            throw new SetupFailed("Before must be called before Then");
        _actor.Before(tearDown ?? throw new SetupFailed("TearDown cannot be null"));
    }

    internal void SetTearDown(Func<Task> tearDown) => SetTearDown(() => Execute(tearDown));

    internal virtual void Arrange() { }

    internal TValue Mention<TValue>(int index) => _context.Mention<TValue>(index);

    internal TValue Create<TValue>() => _context.Create<TValue>();

    internal TValue Create<TValue>([NotNull] Action<TValue> setup)
    {
        if (HasRun)
            throw new SetupFailed("Setup to auto-generated values must be provided before Then");
        return Context.ApplyTo(setup, _context.Create<TValue>());
    }

    internal TValue[] MentionMany<TValue>(int count, int? minCount = null) 
        => _context.MentionMany<TValue>(count, minCount);

    internal TValue Mention<TValue>(string label) => _context.Mention<TValue>(label);

    internal TValue[] MentionMany<TValue>([NotNull] Action<TValue> setup, int count)
        => _context.MentionMany(setup, count);

    internal TValue Mention<TValue>(int index, [NotNull] Action<TValue> setup)
    {
        if (HasRun)
            throw new SetupFailed("Setup to auto-generated values must be provided before Then");
        return _context.Mention(index, setup);
    }

    internal TValue Mention<TValue>(int index, [NotNull] Func<TValue, TValue> setup)
    {
        if (HasRun)
            throw new SetupFailed("Setup to auto-generated values must be provided before Then");
        return _context.Mention(index, setup);
    }

    internal TValue Mention<TValue>(int index, TValue value, bool asDefault = false)
    {
        if (HasRun)
            throw new SetupFailed("Setup to auto-generated values must be provided before Then");
        return _context.Mention(value, index, asDefault);
    }

    internal TValue[] MentionMany<TValue>([NotNull] Action<TValue, int> setup, int count)
        => _context.MentionMany(setup, count);

    private TestResult<TResult> TestResult => _then ??= Run();

    private TestResult<TResult> Run()
    {
        Arrange();
        return _actor.Execute(_context);
    }
}