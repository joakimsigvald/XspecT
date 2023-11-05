using XspecT.Fixture;

namespace XspecT.Internal.Pipelines;

public interface IGivenSubjectTestPipeline<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class;
    IGivenContinuation<TSUT, TResult> And();
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class;
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value);
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value);
}