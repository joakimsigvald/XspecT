using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public interface ITestPipeline<TResult, TSelf>
{
    TestResult<TResult> Then();
    TSelf GivenThat(Action arrangement);
    TSelf Using<TService>(TService service);
    TSelf Using<TService>(Func<TService> service);
}

public interface ITestPipeline<TResult> : ITestPipeline<TResult, ITestPipeline<TResult>>
{
    ITestPipeline<TResult> When(Action act);
    ITestPipeline<TResult> When(Func<TResult> act);
    ITestPipeline<TResult> When(Func<Task> action);
    ITestPipeline<TResult> When(Func<Task<TResult>> func);
}