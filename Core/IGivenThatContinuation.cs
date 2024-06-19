using System.Diagnostics.CodeAnalysis;

namespace XspecT;

/// <summary>
/// A continuation to mock the result of a method invocation
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TReturns"></typeparam>
public interface IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    : IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    /// <summary>
    /// Provide a callback to tap the mocked function call with no input arguments
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap(Action callback);

    /// <summary>
    /// Provide a callback to tap the mocked function call with one input argument
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    /// <param name="callback"></param>
    /// <returns></returns>
    IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg>(Action<TArg> callback);

    /// <summary>
    /// Provide a callback to tap the mocked function call with two input arguments
    /// </summary>
    /// <typeparam name="TArg1"></typeparam>
    /// <typeparam name="TArg2"></typeparam>
    /// <param name="callback"></param>
    /// <returns></returns>
    IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2>(
        Action<TArg1, TArg2> callback);

    /// <summary>
    /// Provide a callback to tap the mocked function call with three input arguments
    /// </summary>
    /// <typeparam name="TArg1"></typeparam>
    /// <typeparam name="TArg2"></typeparam>
    /// <typeparam name="TArg3"></typeparam>
    /// <param name="callback"></param>
    /// <returns></returns>
    IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3>(
        Action<TArg1, TArg2, TArg3> callback);

    /// <summary>
    /// Provide a callback to tap the mocked function call with four input arguments
    /// </summary>
    /// <typeparam name="TArg1"></typeparam>
    /// <typeparam name="TArg2"></typeparam>
    /// <typeparam name="TArg3"></typeparam>
    /// <typeparam name="TArg4"></typeparam>
    /// <param name="callback"></param>
    /// <returns></returns>
    IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4>(
        Action<TArg1, TArg2, TArg3, TArg4> callback);

    /// <summary>
    /// Provide a callback to tap the mocked function call with five input arguments
    /// </summary>
    /// <typeparam name="TArg1"></typeparam>
    /// <typeparam name="TArg2"></typeparam>
    /// <typeparam name="TArg3"></typeparam>
    /// <typeparam name="TArg4"></typeparam>
    /// <typeparam name="TArg5"></typeparam>
    /// <param name="callback"></param>
    /// <returns></returns>
    IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns> Tap<TArg1, TArg2, TArg3, TArg4, TArg5>(
        Action<TArg1, TArg2, TArg3, TArg4, TArg5> callback);
}

/// <summary>
/// Provide a callback to tap the mocked function call with no input arguments
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TReturns"></typeparam>
public interface IGivenThatCommonContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
    /// <summary>
    /// Mock the return-value
    /// </summary>
    /// <param name="returns"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Returns([NotNull] Func<TReturns> returns);

    /// <summary>
    /// Mock the return-value as default
    /// </summary>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> ReturnsDefault();

    /// <summary>
    /// Provide an exception-type to mock
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Throws<TException>() where TException : Exception, new();

    /// <summary>
    /// Provide an exception to mock
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Throws(Func<Exception> ex);
}