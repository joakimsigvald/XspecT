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
    private string _subjectExpr;

    public bool HasRun => _result != null;

    public ITestResult<TResult> Then(string subjectExpr = null)
    {
        _subjectExpr = subjectExpr;
        return TestResult;
    }

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
        Specification.AddGivenSetup<TModel>(setupExpr);
        AssertHasNotRun();
        _context.SetDefault(setup);
    }

    internal void SetDefault<TValue>(Func<TValue, TValue> setup)
    {
        AssertHasNotRun();
        _context.SetDefault(setup);
    }

    internal void SetDefault<TValue>(TValue defaultValue, ApplyTo applyTo, string defaultValuesExpr)
    {
        Specification.AddGiven(defaultValuesExpr, applyTo);
        AssertHasNotRun();
        _context.Use(defaultValue, applyTo);
    }

    internal void PrependSetUp(Action setUp)
    {
        AssertHasNotRun();
        _actor.After(setUp ?? throw new SetupFailed("SetUp cannot be null"));
    }

    internal void PrependSetUp(Func<Task> setUp) => PrependSetUp(() => Execute(setUp));

    internal void SetTearDown(Action tearDown)
    {
        AssertHasNotRun();
        _actor.Before(tearDown ?? throw new SetupFailed("TearDown cannot be null"));
    }

    internal void SetTearDown(Func<Task> tearDown) => SetTearDown(() => Execute(tearDown));

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

    internal void SetTearDown(Action<TSUT> tearDown) => SetTearDown(() => tearDown(_sut));
    internal void SetTearDown(Func<TSUT, Task> tearDown) => SetTearDown(() => tearDown(_sut));
    internal void PrependSetUp(Action<TSUT> setUp) => PrependSetUp(() => setUp(_sut));
    internal void PrependSetUp(Func<TSUT, Task> setUp) => PrependSetUp(() => setUp(_sut));

    private TestResult<TResult> TestResult => _result ??= Run();

    private TestResult<TResult> Run()
    {
        Arrange();
        return _actor.Execute(_sut, _context, _subjectExpr);
    }

    private void AssertHasNotRun()
    {
        if (HasRun)
            throw new SetupFailed("Cannot provide setup after test pipeline was run");
    }

    internal void SetupThrows<TService>(Func<Exception> ex)
    {
        AssertHasNotRun();
        _context.SetupThrows<TService>(ex);
    }
}