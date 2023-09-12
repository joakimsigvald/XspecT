using Moq;
using System.Linq.Expressions;
using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public interface ITestPipeline<TResult>
{
    TestResult<TResult> Then();
    TSpec Then<TSpec>(TSpec spec) where TSpec : SpecBase<TResult>;
    AndVerify<TResult> Then<TService>(Expression<Action<TService>> expression) where TService : class
        => Then().Verify(expression);

    AndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times) where TService : class
        => Then().Verify(expression, times);

    AndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class
        => Then().Verify(expression, times);

    AndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class
        => Then().Verify(expression);

    AndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class
        => Then().Verify(expression, times);

    AndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class
        => Then().Verify(expression, times);
}