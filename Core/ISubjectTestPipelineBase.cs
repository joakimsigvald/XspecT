namespace XspecT;

/// <summary>
/// TODO
/// </summary>
/// <typeparam name="TSUT"></typeparam>
/// <typeparam name="TResult"></typeparam>
public interface ISubjectTestPipelineBase<TSUT, TResult> : ITestPipeline<TResult>
    where TSUT : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult> Given();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult, TService> Given<TService>() where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(Func<TValue> value);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> Given<TValue>(TValue value);
}
