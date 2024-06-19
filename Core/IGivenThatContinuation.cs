using System.Diagnostics.CodeAnalysis;
using XspecT.Internal.Pipelines;

namespace XspecT;

/// <summary>
/// A continuation to mock the result of a method invocation
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TReturns"></typeparam>
public interface IGivenThatContinuation<TSUT, TResult, TService, TReturns>
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
    /// 
    /// </summary>
    /// <typeparam name="TArg"></typeparam>
    /// <param name="callback"></param>
    /// <returns></returns>
    IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg> Tap<TArg>(Action<TArg> callback);
    IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg1, TArg2> Tap<TArg1, TArg2>(
        Action<TArg1, TArg2> callback);

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

/// <summary>
/// 
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TReturns"></typeparam>
/// <typeparam name="TArg"></typeparam>
public interface IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
}

public interface IGivenTappedContinuation<TSUT, TResult, TService, TReturns, TArg1, TArg2>
    : IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TService : class
{
}