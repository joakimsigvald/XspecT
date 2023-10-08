namespace XspecT.Fixture.Pipelines;

public interface ISubjectTestPipeline<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class;
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class;
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value);
    ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func);
}

public interface IGivenSubjectTestPipeline<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class;
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class;
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value);
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value);
}