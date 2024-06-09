using System.Linq.Expressions;

namespace XspecT;

/// <summary>
/// A continuation object to apply additional arrangements to the test-pipeline
/// </summary>
public interface IGivenContinuation<TSUT, TResult, TService>
    where TService : class
{
    /// <summary>
    /// Setup mock to return a value as default for any invocation where no specific mock-setup has been provided
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ReturnsDefault<TReturns>(Func<TReturns> value);

    /// <summary>
    /// Mock the method invocation
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> expression);

    /// <summary>
    /// Provide async method invocation to mock
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="expression"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> expression);
}

/// <summary>
/// A continuation object to apply additional arrangements to the test
/// </summary>
public interface IGivenContinuation<TSUT, TResult>
{
    /// <summary>
    /// Provide a setup-action to be applied when executing the test
    /// </summary>
    /// <param name="setup"></param>
    /// <returns>The test-pipeline</returns>
    IGivenTestPipeline<TSUT, TResult> That(Action setup);
}