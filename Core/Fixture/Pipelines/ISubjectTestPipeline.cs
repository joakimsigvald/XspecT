using Moq;

namespace XspecT.Fixture.Pipelines;

public interface ISubjectTestPipeline<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    IGivenThatSubjectTestPipeline<TSUT, TResult> GivenThat(Action arrangement);
    IGivenThatSubjectTestPipeline<TSUT, TResult> GivenThat<TService>(Action<Mock<TService>> setup) where TService : class;
    ISubjectTestPipeline<TSUT, TResult> Using<TService>(TService service);
    ISubjectTestPipeline<TSUT, TResult> Using<TService>(Func<TService> service);
    ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func);
}

public interface IGivenThatSubjectTestPipeline<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    IGivenThatSubjectTestPipeline<TSUT, TResult> And(Action arrangement);
    IGivenThatSubjectTestPipeline<TSUT, TResult> And<TService>(Action<Mock<TService>> setup) where TService : class;
}