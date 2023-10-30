using System.Linq.Expressions;
using XspecT.Internal.Pipelines;

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

public interface IGivenContinuation<TSUT, TResult> where TSUT : class
{
    IGivenSubjectTestPipeline<TSUT, TResult> That(Action setup);
}