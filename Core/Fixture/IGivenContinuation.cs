using System.Linq.Expressions;
using XspecT.Internal.Pipelines;

namespace XspecT.Fixture;

public interface IGivenContinuation<TSUT, TResult, TService>
    where TSUT : class
    where TService : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression);
}

public interface IGivenContinuation<TSUT, TResult> where TSUT : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> That(Action setup);
}