namespace XspecT;

/// <summary>
/// TODO
/// </summary>
public interface IGivenSubjectTestPipeline<TSUT, TResult> : ISubjectTestPipeline<TSUT, TResult>
    where TSUT : class
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult, TService> And<TService>() where TService : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <returns></returns>
    IGivenContinuation<TSUT, TResult> And();

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="setup"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Action<TValue> setup) where TValue : class;

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(Func<TValue> value);

    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    IGivenSubjectTestPipeline<TSUT, TResult> And<TValue>(TValue value);
}