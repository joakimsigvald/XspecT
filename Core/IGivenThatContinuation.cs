namespace XspecT;

/// <summary>
/// A continuation to mock the result of a method invocation
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TReturns"></typeparam>
public interface IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    /// <summary>
    /// Mock the return-value
    /// </summary>
    /// <param name="returns"></param>
    /// <returns></returns>
    IGivenTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns);

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