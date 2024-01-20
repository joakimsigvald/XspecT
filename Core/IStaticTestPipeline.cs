namespace XspecT;

/// <summary>
/// Handles the test-pipeline specification and execution
/// </summary>
/// <typeparam name="TValue"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IStaticTestPipeline<TValue, TResult> : ITestPipeline<TResult>
{
    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, TResult> act);

    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task> act);

    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue, TResult> When(Func<TValue, Task<TResult>> act);
}

/// <summary>
/// Handles the test-pipeline specification and execution
/// </summary>
/// <typeparam name="TValue1"></typeparam>
/// <typeparam name="TValue2"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IStaticTestPipeline<TValue1, TValue2, TResult> : ITestPipeline<TResult>
{
    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, TResult> act);

    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task> act);

    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TResult> When(Func<TValue1, TValue2, Task<TResult>> act);
}

/// <summary>
/// Handles the test-pipeline specification and execution
/// </summary>
/// <typeparam name="TValue1"></typeparam>
/// <typeparam name="TValue2"></typeparam>
/// <typeparam name="TValue3"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface IStaticTestPipeline<TValue1, TValue2, TValue3, TResult>
    : ITestPipeline<TResult>
{
    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, TResult> act);

    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task> act);

    /// <summary>
    /// Provide the method-to-test
    /// </summary>
    /// <param name="act"></param>
    /// <returns></returns>
    IStaticTestPipeline<TValue1, TValue2, TValue3, TResult> When(Func<TValue1, TValue2, TValue3, Task<TResult>> act);
}