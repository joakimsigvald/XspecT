using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using XspecT.Continuations;
using XspecT.Internal.TestData;
using XspecT.Internal.Verification;

namespace XspecT.Internal.Pipelines;

internal class Pipeline<TSUT, TResult>(Fixture<TSUT> classFixture) : Fixture<TSUT>(classFixture)
{
    private TestResult<TResult> _result;

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
        => Context.ApplyTo(setup, _context.Create<TValue>());

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
        if (_methodUnderTest is not null)
            throw new SetupFailed("Cannot call When twice in the same pipeline");
        _methodUnderTest = new(act ?? throw new SetupFailed("Act cannot be null"), actExpr);
    }

    private TestResult<TResult> TestResult => _result ??= Run();

    private TestResult<TResult> Run()
    {
        PrepareToExecute();
        return Execute();
    }

    private void PrepareToExecute()
    {
        if (_methodUnderTest is null)
            throw new SetupFailed("When must be called before Then");
        if (!_fixture.IsSetUp)
            _fixture.SetUp(Arrange());
        SpecificationGenerator.AddWhen(_methodUnderTest.Expression);
        _fixture.AddToSpecification();
    }

    private TestResult<TResult> Execute()
    {
        const string cue = "could not resolve to an object. (Parameter 'serviceType')";
        try
        {
            var (result, hasResult) = _fixture.Invoke<TResult>(_methodUnderTest);
            return new(result, null, _context, hasResult);
        }
        catch (ArgumentException ex) when (ex.Message.Contains(cue))
        {
            throw new SetupFailed($"Failed to run method under test, because an instance of {ex.Message.Split(cue)[0].Trim()} could not be provided.", ex);
        }
        catch (Exception ex) when (ex is not SetupFailed)
        {
            return new(default, ex, _context, false);
        }
    }

    private void AssertHasNotRun()
    {
        if (_result != null)
            throw new SetupFailed("Cannot provide setup after test pipeline was run");
    }
}