using XspecT.Verification;

namespace XspecT.Fixture.Pipelines;

public interface ITestPipelineBase<TResult, TSelf>
{
    TestResult<TResult> Then();
    TSelf GivenThat(Action arrangement);
    TSelf Using<TService>(TService service);
    TSelf Using<TService>(Func<TService> service);
}

public interface ITestPipeline<TResult> : ITestPipelineBase<TResult, ITestPipeline<TResult>>
{
    ITestPipeline<TResult> When(Action act);
    ITestPipeline<TResult> When(Func<TResult> act);
    ITestPipeline<TResult> When(Func<Task> action);
    ITestPipeline<TResult> When(Func<Task<TResult>> func);
}


public interface IStaticTestPipeline<TResult> : ITestPipelineBase<TResult, IStaticTestPipeline<TResult>>
{
    ITestPipeline<TValue, TResult> Given<TValue>(TValue value);
    ITestPipeline<TValue1, TValue2, TResult> Given<TValue1, TValue2>(TValue1 value1, TValue2 value2);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> Given<TValue1, TValue2, TValue3>(
        TValue1 value1, TValue2 value2, TValue3 value3);

    ITestPipeline<TResult> When(Action act);
    ITestPipeline<TValue, TResult> When<TValue>(Action<TValue> act);
    ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Action<TValue1, TValue2> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Action<TValue1, TValue2, TValue3> act);

    ITestPipeline<TResult> When(Func<TResult> act);
    ITestPipeline<TValue, TResult> When<TValue>(Func<TValue, TResult> act);
    ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, TResult> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(
        Func<TValue1, TValue2, TValue3, TResult> act);

    ITestPipeline<TResult> When(Func<Task> action);
    ITestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task> action);
    ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task> action);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(Func<TValue1, TValue2, TValue3, Task> action);

    ITestPipeline<TResult> When(Func<Task<TResult>> func);
    ITestPipeline<TValue, TResult> When<TValue>(Func<TValue, Task<TResult>> func);
    ITestPipeline<TValue1, TValue2, TResult> When<TValue1, TValue2>(Func<TValue1, TValue2, Task<TResult>> func);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When<TValue1, TValue2, TValue3>(Func<TValue1, TValue2, TValue3, Task<TResult>> func);
}

public interface ITestPipeline<TValue, TResult> : ITestPipelineBase<TResult, ITestPipeline<TValue, TResult>>
{
    ITestPipeline<TValue, TResult> Given(TValue value);
    ITestPipeline<TValue, TResult> When(Action<TValue> act);
    ITestPipeline<TValue, TResult> When(Func<TValue, TResult> act);
    ITestPipeline<TValue, TResult> When(Func<TValue, Task> act);
    ITestPipeline<TValue, TResult> When(Func<TValue, Task<TResult>> act);
}

public interface ITestPipeline<TValue1, TValue2, TResult>
    : ITestPipelineBase<TResult, ITestPipeline<TValue1, TValue2, TResult>>
{
    ITestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2);
    ITestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act);
    ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act);
    ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task> act);
    ITestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task<TResult>> act);
}

public interface ITestPipeline<TValue1, TValue2, TValue3, TResult>
    : ITestPipelineBase<TResult, ITestPipeline<TValue1, TValue2, TValue3, TResult>>
{
    ITestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act);
    ITestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act);
}