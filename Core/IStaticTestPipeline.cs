namespace XspecT;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IStaticTestPipeline<TValue, TResult> : ITestPipeline<TResult>
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> Given(TValue value);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> When(Action<TValue> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, TResult> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task<TResult>> act);
}

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TValue1"></typeparam>
/// <typeparam name="TValue2"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IStaticTestPipeline<TValue1, TValue2, TResult>
    : ITestPipeline<TResult>
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> Given(TValue1 value1, TValue2 value2);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Action<TValue1, TValue2> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task<TResult>> act);
}

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TValue1"></typeparam>
/// <typeparam name="TValue2"></typeparam>
/// <typeparam name="TValue3"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IStaticTestPipeline<TValue1, TValue2, TValue3, TResult>
    : ITestPipeline<TResult>
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <param name="value3"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> Given(TValue1 value1, TValue2 value2, TValue3 value3);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Action<TValue1, TValue2, TValue3> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act);

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act);
}