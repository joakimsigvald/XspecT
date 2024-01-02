using System.Linq.Expressions;

namespace XspecT;

/// <summary>
/// A continuation object to apply additional arrangements to the test-pipeline
/// </summary>
public interface IGivenContinuation<TSUT, TResult, TService>
    where TSUT : class
    where TService : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> ReturnsDefault<TReturns>(Func<TReturns> value);

    /// <summary>
    /// Provide method invocation to mock
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
/// A continuation object to apply additional arrangements to the test-pipeline
/// </summary>
public interface IGivenContinuation<TSUT, TResult> where TSUT : class
{
    /// <summary>
    /// Provide a setup-action to be applied when running the test-pipeline
    /// </summary>
    /// <param name="setup"></param>
    /// <returns>The test-pipeline</returns>
    IGivenSubjectTestPipeline<TSUT, TResult> That(Action setup);
}