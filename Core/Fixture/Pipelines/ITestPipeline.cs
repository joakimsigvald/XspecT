using Moq;
using System.Linq.Expressions;
using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public interface ITestPipeline<TResult>
{
    ITestResult<TResult> Then();
    TSubject Then<TSubject>(TSubject subject);
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression) where TService : class;
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Times times) where TService : class;
    IAndVerify<TResult> Then<TService>(Expression<Action<TService>> expression, Func<Times> times) where TService : class;
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression) where TService : class;
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Times times)
        where TService : class;
    IAndVerify<TResult> Then<TService, TReturns>(Expression<Func<TService, TReturns>> expression, Func<Times> times)
        where TService : class;
}