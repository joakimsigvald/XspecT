using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Continuations;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class Pipeline<TSUT, TResult> : Fixture<TSUT>
{
    private readonly SpecActor<TSUT, TResult> _actor;
    private TestResult<TResult> _result;

    internal Pipeline(Fixture<TSUT> classFixture = null)
        : base(classFixture) => _actor = new(_fixture);

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
}