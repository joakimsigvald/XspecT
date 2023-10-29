using XspecT.Internal.Pipelines;

namespace XspecT.Fixture;

public interface IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns);
    IGivenSubjectTestPipeline<TSUT, TResult> Throws<TException>() where TException : Exception, new();
    IGivenSubjectTestPipeline<TSUT, TResult> Throws(Func<Exception> ex);
}