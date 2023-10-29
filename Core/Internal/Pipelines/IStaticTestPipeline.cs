namespace XspecT.Internal.Pipelines;

public interface IStaticTestPipeline<TValue, TResult> : ITestPipeline<TResult>
{
    IStaticTestPipeline<TValue, TResult> Given(TValue value);
    IStaticTestPipeline<TValue, TResult> When(Action<TValue> act);
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, TResult> act);
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task> act);
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task<TResult>> act);
}

public interface IStaticTestPipeline<TValue1, TValue2, TResult>
    : ITestPipeline<TResult>
{
    IStaticTestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2);
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act);
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act);
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task> act);
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task<TResult>> act);
}

public interface IStaticTestPipeline<TValue1, TValue2, TValue3, TResult>
    : ITestPipeline<TResult>
{
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3);
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act);
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act);
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act);
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act);
}