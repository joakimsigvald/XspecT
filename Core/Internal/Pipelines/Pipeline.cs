using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Continuations;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class Pipeline<TSUT, TResult>
{
    private readonly Context _context = new();
    private readonly SpecActor<TSUT, TResult> _actor = new();
    private TestResult<TResult> _result;
    private readonly Arranger _arranger = new();
    //private TSUT _sut;

    internal bool HasRun => _result != null;

    internal ITestResult<TResult> Then() => TestResult;

    internal IAndVerify<TResult> Then<TService>(
        Expression<Action<TService>> expression, string expressionExpr) 
        where TService : class
        => TestResult.Verify(expression, expressionExpr);

    internal IAndVerify<TResult> Then<TService>(
        Expression<Action<TService>> expression, Times times, string expressionExpr) where TService : class
        => TestResult.Verify(expression, times, expressionExpr);

    internal IAndVerify<TResult> Then<TService>(
        Expression<Action<TService>> expression, Func<Times> times, string expressionExpr) where TService : class
        => TestResult.Verify(expression, times, expressionExpr);

    internal IAndVerify<TResult> Then<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, string expressionExpr) where TService : class
        => TestResult.Verify(expression, expressionExpr);

    internal IAndVerify<TResult> Then<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, Times times, string expressionExpr)
        where TService : class
        => TestResult.Verify(expression, times, expressionExpr);

    internal IAndVerify<TResult> Then<TService, TReturns>(
        Expression<Func<TService, TReturns>> expression, Func<Times> times, string expressionExpr)
        where TService : class
        => TestResult.Verify(expression, times, expressionExpr);

    internal void SetDefault<TModel>(
        Action<TModel> setup, string setupExpr = null) where TModel : class
    {
        SpecificationGenerator.AddGiven<TModel>(setupExpr);
        AssertHasNotRun();
        _context.SetDefault(setup);
    }

    internal void SetDefault<TValue>(
        Func<TValue, TValue> transform, string transformExpr)
    {
        SpecificationGenerator.AddGiven<TValue>(transformExpr);
        AssertHasNotRun();
        _context.SetDefault(transform);
    }

    internal void SetDefault<TValue>(TValue defaultValue, ApplyTo applyTo, string defaultValuesExpr)
    {
        SpecificationGenerator.AddGiven(defaultValuesExpr, applyTo);
        AssertHasNotRun();
        _context.Use(defaultValue, applyTo);
    }

    internal void PrependSetUp(Delegate setUp, string setUpExpr)
    {
        AssertHasNotRun();
        _actor.After(new(setUp ?? throw new SetupFailed("SetUp cannot be null"), setUpExpr));
    }

    internal void SetTearDown(Delegate tearDown, string tearDownExpr)
    {
        AssertHasNotRun();
        _actor.Before(new(tearDown ?? throw new SetupFailed("TearDown cannot be null"), tearDownExpr));
    }

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

    internal Lazy<TSUT> Arrange()
    {
        _arranger.Arrange();
        return new Lazy<TSUT>(_context.CreateSUT<TSUT>);
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
        _actor.When(new(act ?? throw new SetupFailed("Act cannot be null"), actExpr));
    }

    private TestResult<TResult> TestResult => _result ??= Run();

    private TestResult<TResult> Run() => _actor.Execute(Arrange(), _context);

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