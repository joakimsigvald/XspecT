using Moq;
using XspecT.Fixture.Exceptions;

namespace XspecT.Fixture.Pipelines;

public interface ISubjectTestPipeline<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    IGivenSubjectTestPipeline<TSUT, TResult> GivenThat(Action arrangement);
    IGivenSubjectTestPipeline<TSUT, TResult> GivenThat<TService>(Action<Mock<TService>> setup) where TService : class;
    IGivenSubjectTestPipeline<TSUT, TResult> GivenThe<TValue>(Action<TValue> setup) where TValue : class;
    ISubjectTestPipeline<TSUT, TResult> Using<TService>(TService service);
    ISubjectTestPipeline<TSUT, TResult> Using<TService>(Func<TService> service);
    ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func);
}

public interface IGivenSubjectTestPipeline<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    IGivenSubjectTestPipeline<TSUT, TResult> And(Action arrangement);
    IGivenSubjectTestPipeline<TSUT, TResult> And<TService>(Action<Mock<TService>> setup) where TService : class;
    IGivenSubjectTestPipeline<TSUT, TResult> AndThe<TValue>(Action<TValue> setup) where TValue : class;
}