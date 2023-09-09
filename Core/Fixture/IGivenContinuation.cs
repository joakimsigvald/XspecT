using System.Linq.Expressions;

namespace XspecT.Fixture;

public interface IGivenContinuation<TSUT, TResult, TService>
    where TSUT : class
    where TService : class
{
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression);
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression);
}