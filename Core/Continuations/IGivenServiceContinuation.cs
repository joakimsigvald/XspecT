using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace XspecT.Continuations;

/// <summary>
/// A continuation object to apply additional arrangements to the test-pipeline
/// </summary>
public interface IGivenServiceContinuation<TSUT, TResult, TService>
    where TService : class
{
    /// <summary>
    /// Setup mock to return a value as default for any invocation where no specific mock-setup has been provided
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns<TReturns>(Func<TReturns> value);

    /// <summary>
    /// Setup mock to throw an exception for any call, unless otherwise specified
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Throws<TException>() where TException : Exception, new();

    /// <summary>
    /// Setup mock to throw the given exception for any call
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Throws(Func<Exception> ex);

    /// <summary>
    /// Mock the method invocation
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="call"></param>
    /// <param name="callExpr"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, TReturns>> call,
        [CallerArgumentExpression(nameof(call))] string callExpr = null);

    /// <summary>
    /// Provide async method invocation to mock
    /// </summary>
    /// <typeparam name="TReturns"></typeparam>
    /// <param name="call"></param>
    /// <param name="callExpr"></param>
    /// <returns>Continuation for providing method invocation result to mock</returns>
    IGivenThatContinuation<TSUT, TResult, TService, TReturns> That<TReturns>(
        Expression<Func<TService, Task<TReturns>>> call,
        [CallerArgumentExpression(nameof(call))] string callExpr = null);
}