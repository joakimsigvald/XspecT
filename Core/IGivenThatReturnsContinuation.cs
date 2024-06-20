using System.Linq.Expressions;

namespace XspecT;

/// <summary>
/// Provide a callback to tap the mocked function call with no input arguments
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TService"></typeparam>
public interface IGivenThatReturnsContinuation<TSUT, TResult, TService> : IGivenTestPipeline<TSUT, TResult>
    where TService : class
{
    /// <summary>
    /// Setup mock to return a value as default for any invocation where no specific mock-setup has been provided
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> AndReturnsDefault<TReturns>(Func<TReturns> value);

    /// <summary>
    /// Mock the method invocation
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> AndThat<TReturns>(
        Expression<Func<TService, TReturns>> expression);

    /// <summary>
    /// Provide async method invocation to mock
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> AndThat<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression);
}