using XspecT.Internal.Pipelines;

namespace XspecT.Fixture;

public interface IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="returns"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Throws<TException>() where TException : Exception, new();

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Throws(Func<Exception> ex);
}