namespace XspecT.Fixture.Pipelines;

public interface IUsingSubjectTestPipeline<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    IUsingSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value);
    IUsingSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value);
}