using XspecT.Fixture.Pipelines;

namespace XspecT.Fixture;

public class UsingSubjectTestPipeline<TSUT, TResult>
    : SubjectTestPipeline<TSUT, TResult>, IUsingSubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    public UsingSubjectTestPipeline(SubjectSpec<TSUT, TResult> parent) : base(parent) { }
    public IUsingSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value) => Using(value);
    public IUsingSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value) => Using(value);
}