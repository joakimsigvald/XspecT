namespace XspecT;

/// <summary>
/// TODO
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
    /// Provide a return-value to mock
    /// </summary>
    /// <param name="returns"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns);

    /// <summary>
    /// Provide an exception-type to mock
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Throws<TException>() where TException : Exception, new();

    /// <summary>
    /// Provide an exception to mock
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Throws(Func<Exception> ex);
}