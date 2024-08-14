using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Continuations;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;
using static XspecT.Internal.Pipelines.AsyncHelper;

namespace XspecT.Internal.Pipelines;

internal class Pipeline<TSUT, TResult>
{
    private readonly Context _context = new();
    private readonly SpecActor<TSUT, TResult> _actor = new();
    private TestResult<TResult> _result;
    private readonly Arranger _arranger = new();
    private TSUT _sut;

    public bool HasRun => _result != null;

    public ITestResult<TResult> Then() => TestResult;

    public IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, string expressionExpr = null) 
        where TService : class
        => TestResult.Verify(expression, expressionExpr);

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

    internal void SetDefault<TModel>(
        Action<TModel> setup, string setupExpr = null) where TModel : class
    {
        Specification.AddGiven<TModel>(setupExpr);
        AssertHasNotRun();
        _context.SetDefault(setup);
    }

    internal void SetDefault<TValue>(
        Func<TValue, TValue> transform, string transformExpr = null)
    {
        Specification.AddGiven<TValue>(transformExpr);
        AssertHasNotRun();
        _context.SetDefault(transform);
    }

    internal void SetDefault<TValue>(TValue defaultValue, ApplyTo applyTo, string defaultValuesExpr)
    {
        Specification.AddGiven(defaultValuesExpr, applyTo);
        AssertHasNotRun();
        _context.Use(defaultValue, applyTo);
    }

    internal void PrependSetUp(Func<Task> setUp, string setUpExpr)
        => PrependSetUp(() => Execute(setUp), setUpExpr);

    internal void PrependSetUp(Action setUp, string setUpExpr)
    {
        AssertHasNotRun();
        _actor.After(setUp ?? throw new SetupFailed("SetUp cannot be null"), setUpExpr);
    }

    internal void SetTearDown(Action tearDown, string tearDownExpr)
    {
        AssertHasNotRun();
        _actor.Before(tearDown ?? throw new SetupFailed("TearDown cannot be null"), tearDownExpr);
    }

    internal void SetTearDown(Func<Task> tearDown, string tearDownExpr) 
        => SetTearDown(() => Execute(tearDown), tearDownExpr);

    internal TValue Mention<TValue>(int index = 0) 
        => index < 0 ? _context.Create<TValue>() : _context.Mention<TValue>(index);

    internal TValue Create<TValue>([NotNull] Action<TValue> setup)
    {
        AssertHasNotRun();
        return Context.ApplyTo(setup, _context.Create<TValue>());
    }

    internal TValue[] MentionMany<TValue>(int count, int? minCount = null)
        => _context.MentionMany<TValue>(count, minCount);

    internal TValue[] MentionMany<TValue>([NotNull] Action<TValue> setup, int count)
        => _context.MentionMany(setup, count);

    internal TValue Mention<TValue>(int index, [NotNull] Action<TValue> setup)
    {
        AssertHasNotRun();
        return _context.Mention(index, setup);
    }

    internal TValue Mention<TValue>(int index, [NotNull] Func<TValue, TValue> setup)
    {
        AssertHasNotRun();
        return _context.Mention(index, setup);
    }

    internal TValue Mention<TValue>(int index, TValue value)
    {
        AssertHasNotRun();
        return _context.Mention(value, index);
    }

    internal TValue[] MentionMany<TValue>([NotNull] Action<TValue, int> setup, int count)
        => _context.MentionMany(setup, count);

    internal void Arrange()
    {
        _arranger.Arrange();
        _sut = _context.CreateSUT<TSUT>();
    }

    internal Mock<TObject> GetMock<TObject>() where TObject : class 
        => _context.GetMock<TObject>();

    internal void ArrangeFirst(Action arrangement)
    {
        AssertHasNotRun();
        _arranger.Push(arrangement);
    }

    internal void ArrangeLast(Action arrangement)
    {
        AssertHasNotRun();
        _arranger.Add(arrangement);
    }

    internal void SetAction(Delegate act, string actExpr)
    {
        AssertHasNotRun();
        _actor.When(act ?? throw new SetupFailed("Act cannot be null"), actExpr);
    }

    internal void SetTearDown(Action<TSUT> tearDown, string tearDownExpr) 
        => SetTearDown(() => tearDown(_sut), tearDownExpr);
    internal void SetTearDown(Func<TSUT, Task> tearDown, string tearDownExpr) 
        => SetTearDown(() => tearDown(_sut), tearDownExpr);
    internal void PrependSetUp(Action<TSUT> setUp, string setUpExpr) 
        => PrependSetUp(() => setUp(_sut), setUpExpr);
    internal void PrependSetUp(Func<TSUT, Task> setUp, string setUpExpr) 
        => PrependSetUp(() => setUp(_sut), setUpExpr);

    private TestResult<TResult> TestResult => _result ??= Run();

    private TestResult<TResult> Run()
    {
        Arrange();
        return _actor.Execute(_sut, _context);
    }

    private void AssertHasNotRun()
    {
        if (HasRun)
            throw new SetupFailed("Cannot provide setup after test pipeline was run");
    }

    internal void SetupThrows<TService>(Func<Exception> expected)
    {
        AssertHasNotRun();
        _context.SetupThrows<TService>(expected);
    }
}