using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

public interface IGivenThatContinuation<TSUT, TResult, TService, TReturns>
    where TSUT : class
    where TService : class
{
    IGivenSubjectTestPipeline<TSUT, TResult> Returns(Func<TReturns> returns);
}