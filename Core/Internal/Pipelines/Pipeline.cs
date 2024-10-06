using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Continuations;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class Pipeline<TSUT, TResult> : IDisposable
{
    private bool _disposed;
    private readonly Context _context;
    private readonly SpecFixture<TSUT, TResult> _fixture;
    private readonly SpecActor<TSUT, TResult> _actor;
    private TestResult<TResult> _result;
    private readonly Arranger _arranger;

    internal Pipeline(Pipeline<TSUT, TResult> fixture = null)
    {
        _context = fixture?._context ?? new();
        _fixture = fixture?._fixture ?? new();
        _actor = new(_fixture);
        _arranger = fixture?._arranger ?? new();
    }

    ~Pipeline() => Dispose(false);

    internal bool HasRun => _result != null;

    internal ITestResult<TResult> Then() => TestResult;

    internal Pipeline<TSUT, TResult> AsFixture() => new(this);

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
        AssertIsNotSetUp();
        _context.SetDefault(setup);
    }

    internal void SetDefault<TValue>(
        Func<TValue, TValue> transform, string transformExpr)
    {
        SpecificationGenerator.AddGiven<TValue>(transformExpr);
        AssertIsNotSetUp();
        _context.SetDefault(transform);
    }

    internal void SetDefault<TValue>(TValue defaultValue, ApplyTo applyTo, string defaultValuesExpr)
    {
        SpecificationGenerator.AddGiven(defaultValuesExpr, applyTo);
        AssertIsNotSetUp();
        _context.Use(defaultValue, applyTo);
    }

    internal void PrependSetUp(Delegate setUp, string setUpExpr)
    {
        AssertIsNotSetUp();
        _fixture.After(new(setUp ?? throw new SetupFailed("SetUp cannot be null"), setUpExpr));
    }

    internal void SetTearDown(Delegate tearDown, string tearDownExpr)
    {
        AssertIsNotSetUp();
        _fixture.Before(new(tearDown ?? throw new SetupFailed("TearDown cannot be null"), tearDownExpr));
    }

    internal void TearDown() => _fixture.TearDown();

    internal TValue Mention<TValue>(int index = 0)
        => index < 0 ? _context.Create<TValue>() : _context.Mention<TValue>(index);

    internal TValue Create<TValue>([NotNull] Action<TValue> setup)
    {
        return Context.ApplyTo(setup, _context.Create<TValue>());
    }

    internal TValue Mention<TValue>(int index, [NotNull] Action<TValue> setup)
    {
        AssertHasNotRun();
        return _context.Mention(index, setup);
    }

    internal TValue Mention<TValue>(int index, [NotNull] Func<TValue, TValue> transform)
    {
        AssertHasNotRun();
        return _context.Mention(index, transform);
    }

    internal TValue Mention<TValue>(int index, TValue value)
    {
        AssertHasNotRun();
        return _context.Mention(value, index);
    }

    internal TValue[] MentionMany<TValue>(int count, int? minCount = null)
        => _context.MentionMany<TValue>(count, minCount);

    internal TValue[] MentionMany<TValue>([NotNull] Action<TValue> setup, int count)
        => _context.MentionMany(setup, count);

    internal TValue[] MentionMany<TValue>([NotNull] Func<TValue, TValue> transform, int count)
        => _context.MentionMany(transform, count);

    internal TValue[] MentionMany<TValue>([NotNull] Func<TValue, int, TValue> transform, int count)
        => _context.MentionMany(transform, count);

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
        AssertIsNotSetUp();
        _arranger.Push(arrangement);
    }

    internal void ArrangeLast(Action arrangement)
    {
        AssertIsNotSetUp();
        _arranger.Add(arrangement);
    }

    internal void SetAction(Delegate act, string actExpr)
    {
        AssertHasNotRun();
        _actor.When(new(act ?? throw new SetupFailed("Act cannot be null"), actExpr));
    }

    private TestResult<TResult> TestResult => _result ??= Run();

    private TestResult<TResult> Run()
    {
        if (!_fixture.IsSetUp)
            _fixture.SetUp(Arrange());
        return _actor.Execute(_context);
    }

    private void AssertHasNotRun()
    {
        if (HasRun)
            throw new SetupFailed("Cannot provide setup after test pipeline was run");
    }

    private void AssertIsNotSetUp()
    {
        if (_fixture.IsSetUp)
            throw new SetupFailed("Cannot provide setup after pipeline is set up");
    }

    internal void SetupThrows<TService>(Func<Exception> expected)
    {
        AssertIsNotSetUp();
        _context.SetupThrows<TService>(expected);
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Calls any teardown methods provided in the test pipeline with the method `Before`.
    /// Override this method to perform custom teardown in your test class.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;
        if (disposing)
            TearDown();
        _disposed = true;
    }
}