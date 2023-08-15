namespace XspecT.Fixture.Pipelines;

public interface ISubjectTestPipeline<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    ISubjectTestPipeline<TSUT, TResult> GivenThat(Action arrangement);
    ISubjectTestPipeline<TSUT, TResult> Using<TService>(TService service);
    ISubjectTestPipeline<TSUT, TResult> Using<TService>(Func<TService> service);
    ISubjectTestPipeline<TSUT, TResult> When(Action<TSUT> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, TResult> act);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task> action);
    ISubjectTestPipeline<TSUT, TResult> When(Func<TSUT, Task<TResult>> func);
}