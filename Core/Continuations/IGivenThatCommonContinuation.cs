using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace XspecT.Continuations;

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
    /// <param name="returnsExpr"></param>
    /// <returns></returns>
    IGivenThatReturnsContinuation<TSUT, TResult, TService> Returns(
        [NotNull] Func<TReturns> returns, [CallerArgumentExpression(nameof(returns))] string returnsExpr = null);

    /// <summary>
    /// Mock the return-value as default
    /// </summary>
    /// <returns></returns>
    IGivenThatReturnsContinuation<TSUT, TResult, TService> ReturnsDefault();

    /// <summary>
    /// Provide an exception-type to mock
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <returns></returns>
    IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws<TException>() where TException : Exception, new();

    /// <summary>
    /// Provide an exception to mock
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    IGivenThatReturnsContinuation<TSUT, TResult, TService> Throws(Func<Exception> ex);
}